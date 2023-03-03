using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PiNetworkNet;
using WePi.Domain;

namespace WePi
{
    public class PiPaymentProcessor
    {
        private PiPaymentManager _paymentManager;
        public PiPaymentProcessor(PiPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        public async Task<List<PiPayment>> GetNewPaymentsAsync()
        {
            return await _paymentManager.GetNewPaymentsAsync();
        }

        public async Task<List<PiPayment>> GetCreatedPaymentsAsync()
        {
            return await _paymentManager.GetCreatedPaymentsAsync();
        }

        public async Task<PiPayment> GetRelatePayment(PaymentDto dto)
        {
            Guid? id = null;
            if (dto.Metadata != null)
            {
                id = dto.Metadata.Id;
            }
            var payment = await _paymentManager.GetByIdOrIdentifierAsync(dto.Identifier, id);
            return payment;
        }

        public async Task<PiPayment> IsValidDto(PaymentDto dto)
        {
            Guid? id = null;
            if (dto.Metadata != null)
            {
                id = dto.Metadata.Id;
            }
            var payment = await _paymentManager.GetByIdOrIdentifierAsync(dto.Identifier, id);
            return payment;
        }

        public async Task<PiPayment> CreateTransaction(PiPayment payment, PaymentDto dto)
        {
            payment = UpdateDto(payment, dto);
            payment.Step = 2;
            var pay = await _paymentManager.UpdateTransaction(payment);
            return pay;
        }

        public async Task<PiPayment> UpdateTransaction(PiPayment payment)
        {
            payment.Step = 2;
            return await _paymentManager.UpdateTransaction(payment);
        }

        public async Task<PiPayment> UpdateTransaction(PiPayment payment, string txid)
        {
            payment.Step = 3;
            var pay = await _paymentManager.UpdateTransaction(payment, txid);
            return pay;
        }

        public async Task<PiPayment> CompleteTransaction(PiPayment payment, PaymentDto dto)
        {
            payment = UpdateDto(payment, dto);
            if (payment.Completed)
            {
                payment.Finished = true;
                payment.Step = 4;
            }
            var pay = await _paymentManager.UpdateTransaction(payment);
            return pay;
        }

        public PiPayment UpdateDto(PiPayment payment, PaymentDto dto)
        {
            if (dto.Transaction != null)
            {
                payment.TxId = dto.Transaction.TxId;
                payment.TxLink = dto.Transaction.Link;
                payment.TxVerified = dto.Transaction.Verified;
            }
            payment.Identifier = dto.Identifier;
            payment.UserUid = dto.Useruid;
            payment.FromAddress = dto.FromAddress;
            payment.ToAddress = dto.ToAddress;
            payment.Network = dto.Network;
            payment.Direction = dto.Direction;
            payment.Amount = dto.Amount;
            payment.Approved = dto.Status.DeveloperApproved;
            payment.Completed = dto.Status.DeveloperCompleted;
            payment.Cancelled = dto.Status.Cancelled;
            payment.UserCancelled = dto.Status.UserCancelled;
            payment.Verified = dto.Status.TransactionVerified;
            payment.CreatedAt = dto.CreatedAt.UtcDateTime;
            payment.Memo = dto.Memo;
            return payment;
        }
    }
}
