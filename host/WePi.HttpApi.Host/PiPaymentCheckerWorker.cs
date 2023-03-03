using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

using Volo.Abp;
using Volo.Abp.Uow;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

using stellar_dotnet_sdk;

using PiNetworkNet;
using WePi.Domain;

namespace WePi
{    
    public class PiPaymentCheckerWorker : AsyncPeriodicBackgroundWorkerBase
    {
        IConfiguration _configuration;
        private string Token;
        private string Seed;
        private KeyPair _keypair;
        private PiPaymentProcessor _processor;
        public PiPaymentCheckerWorker(
                AbpAsyncTimer timer,
                IConfiguration configuration,
                IServiceScopeFactory serviceScopeFactory
            ) : base(
                timer,
                serviceScopeFactory)
        {
            // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Timer.Period = 60000; //1 minutes
            _configuration = configuration;
            Token = _configuration["PiNetwork:API"];
            Seed = _configuration["PiNetwork:Seed"];
        }

        [UnitOfWork]
        protected async override Task DoWorkAsync(
            PeriodicBackgroundWorkerContext workerContext)
        {
            if (Token == null || Seed == null)
            {
                Logger.LogInformation("WePi - Starting, API Key or Seed App Wallet is null, ignore ...");
                return;
            }
            Logger.LogInformation("WePi - Checking status of payments...");
            PiNetworkClient client = new PiNetworkClient(Token);
            PiPaymentManager _paymentManager;
            try
            {
                _keypair = KeyPair.FromSecretSeed(Seed);
                _paymentManager = workerContext
                    .ServiceProvider
                    .GetRequiredService<PiPaymentManager>();
            }
            catch (Exception e)
            {
                Logger.LogInformation($"WePi - Initialize error: {e.Message}");
                return;
            }
            _processor = new PiPaymentProcessor(_paymentManager);
            try
            {
                await ProcessIncompleteServerPayments(client);
                await ProcessCreatedPayments(client);
                await ProcessNewPayments(client);
            }
            catch (Exception e)
            {
                Logger.LogInformation($"WePi - Completed error: {e.Message}");
            }

            _processor = null;
            _paymentManager = null;
        }



        protected async Task ProcessNewPayments(PiNetworkClient client)
        {
            var lstPays = await _processor.GetNewPaymentsAsync();
            if (lstPays == null) { return; }
            while (lstPays.Count > 0) 
            {
                var pay = lstPays.ElementAt(0);
                var payTmp = pay;
                var args = new PaymentArgs()
                {
                    Amount = pay.Amount,
                    Uid = pay.UserUid,
                    Memo = $"{pay.Memo}",
                    Metadata = new Metadata() { Id = pay.Id, Category="withdraw", },
                };
                var create = new CreatePaymentDto()
                {
                    Payment = args,
                };
                PaymentDto dto;
                bool ok = false;
                try
                {
                    dto = await client.Create(create);
                    ok = true;
                    lstPays.RemoveAt(0);
                }
                catch (PiNetworkException ex)
                {
                    dto = ex.PiError.Payment;
                }
                catch { throw; }
                if (dto == null) { return; }
                if (!ok)
                {
                    pay = await _processor.GetRelatePayment(dto);
                    payTmp = pay;
                    Logger.LogInformation($"WePi - {dto.Identifier} process old payment, id {pay.Id}");
                }
                else
                {
                    payTmp.Step = 2;
                    Logger.LogInformation($"WePi - {dto.Identifier} create new payment success, id {pay.Id}");
                }
                await ProcessPaymentPairsAsync(client, payTmp, dto);
            }
        }

        protected async Task ProcessCreatedPayments(PiNetworkClient client)
        {
            try
            {
                var lstPays = await _processor.GetCreatedPaymentsAsync();
                if (lstPays == null)
                {
                    return;
                }
                foreach (var pay in lstPays)
                {
                    Logger.LogInformation($"WePi - {pay.Identifier} process created payment, step: {pay.Step}");
                    var payTmp = pay;
                    PaymentDto dto;
                    try
                    {
                        dto = await client.Get(pay.Identifier);
                        if (dto == null)
                        {
                            Logger.LogInformation($"WePi - {pay.Identifier} payment dto not found");
                            payTmp.Finished = true;
                            payTmp.Step = 7;
                            await _processor.UpdateTransaction(payTmp);
                            continue;
                        }
                    }
                    catch (PiNetworkException ex)
                    {
                        dto = ex.PiError.Payment;
                        throw;
                    }
                    await ProcessPaymentPairsAsync(client, payTmp, dto);
                }                
            }
            catch
            {
                throw;
            }
            await Task.CompletedTask;
        }

        protected async Task ProcessIncompleteServerPayments(PiNetworkClient client)
        {
            var dtos = await client.GetIncompleteServerPayments();
            if (dtos == null) return;
            if (dtos.Count == 0) return;
            foreach (var dto in dtos)
            {
                Logger.LogInformation($"WePi - {dto.Identifier} process incomplete payment");
                if (dto.Status.UserCancelled) continue;
                if (dto.Direction != "app_to_user") continue;
                var payment = await _processor.GetRelatePayment(dto);
                if (payment == null)
                {
                    continue;
                }
                await ProcessPaymentPairsAsync(client, payment, dto);
            }
        }

        protected async Task ProcessPaymentPairsAsync(PiNetworkClient client, PiPayment pay, PaymentDto dto)
        {
            // Step: 0 - Start, 1 - Approved, 2 - CreatedPayment + Identifier, 3 - Make transaction success, 4 - Complete
            var payTmp = pay;

            Logger.LogInformation($"WePi - {dto.Identifier} process payment pairs {pay.Id} {pay.Step} {JsonConvert.SerializeObject(dto)}");
            
            if (string.IsNullOrEmpty(dto.Identifier)) return;
            if (dto.Status.Cancelled == true || dto.Status.UserCancelled == true || dto.Status.DeveloperCompleted == true)
            {
                Logger.LogInformation($"WePi - {pay.Identifier} process finished payment");
                payTmp = _processor.UpdateDto(payTmp, dto);
                payTmp.Finished = true;
                if (dto.Status.DeveloperCompleted == true)
                    payTmp.Step = 4;
                else
                    payTmp.Step = 8;
                await _processor.UpdateTransaction(payTmp);
                return;
            }            

            string txHash = payTmp.TxId;
            if (string.IsNullOrEmpty(txHash))
            {
                if (dto.Transaction != null)
                {
                    txHash = dto.Transaction.TxId;
                }    
            }    
            if (string.IsNullOrEmpty(payTmp.Identifier))
            {
                payTmp = _processor.UpdateDto(payTmp, dto);
                payTmp = await _processor.UpdateTransaction(payTmp);
            }
            if (string.IsNullOrEmpty(txHash))
            {
                if (dto.Transaction!= null)
                {
                    if (!string.IsNullOrEmpty(dto.Transaction.TxId))
                    {
                        _processor.UpdateDto(payTmp, dto);
                        await _processor.CompleteTransaction(payTmp, dto);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(dto.ToAddress)) { return; }
                var payment = payTmp;
                var data = new TransactionData()
                {
                    FromAddress = _keypair.AccountId,
                    ToAddress = dto.ToAddress,
                    Amount = dto.Amount,
                    Identifier = dto.Identifier,
                };
                var Id = payment.Id;
                Logger.LogInformation($"WePi - {dto.Identifier} ready to send transaction, id {Id}");
                var tx = await client.SendNativeAssets(dto.Network, Seed, data);
                if (!tx.IsSuccess())
                {
                    throw new Exception($"WePi - {dto.Identifier} error transaction");
                }
                txHash = tx.Hash;
                Logger.LogInformation($"WePi - {dto.Identifier} sent txid {tx.Hash}");
                payment.Step = 3;
                payTmp = await _processor.UpdateTransaction(payment, tx.Hash);

            }
            if (dto.Status.DeveloperCompleted == false && !string.IsNullOrEmpty(txHash))
            {
                PaymentDto dto_result = null;
                try
                {
                    dto_result = await client.Complete(dto.Identifier, txHash);
                    Logger.LogInformation($"WePi - {dto.Identifier} call complete on server success !");
                }
                catch (PiNetworkException ex)
                {
                    if (ex.PiError.Payment != null && ex.PiError.ErrorName == "already_completed")
                    {
                        Logger.LogInformation($"WePi - {dto.Identifier} complete server already_completed.");
                        dto_result = ex.PiError.Payment;
                    }
                }
                if (dto_result == null)
                {
                    Logger.LogInformation($"WePi - {dto.Identifier} complete server error.");
                    return;
                }
                payTmp = await _processor.CompleteTransaction(payTmp, dto_result);
                if (payTmp != null)
                {
                    Logger.LogInformation($"WePi - {dto.Identifier} complete local success.");
                }
                else
                {
                    Logger.LogInformation($"WePi - {dto.Identifier} complete local error.");
                    return;
                }
            }
            else
            {
                Logger.LogInformation($"WePi - {dto.Identifier}, Unknown {JsonConvert.SerializeObject(dto)}");
            }
            await Task.CompletedTask;
        }
    }
}
