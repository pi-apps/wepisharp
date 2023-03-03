using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using WePi.Domain;

namespace WePi.PiPayments;

public class PiPaymentAppService : WePiAppService, IPiPaymentAppService
{
    protected readonly PiPaymentManager _manager;
    //protected readonly PiPaymentRepository
    protected readonly IRepository<PiPayment, Guid> _paymentRepository;
     public PiPaymentAppService(
        PiPaymentManager manager,
        IRepository<PiPayment, Guid> paymentRepository
        )
    {
        _manager = manager;
        _paymentRepository = paymentRepository;
    }
    public async Task<PiPaymentDto> GetAsync()
    {
        var tmp = await _manager.GetCreatedPaymentsAsync();
        return 
            new PiPaymentDto
            {
                Value = 42
            }
        ;
    }

    [Authorize]
    public Task<PiPaymentDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new PiPaymentDto
            {
                Value = 42
            }
        );
    }
}
