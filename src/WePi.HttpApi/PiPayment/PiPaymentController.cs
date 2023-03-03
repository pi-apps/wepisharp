using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace WePi.PiPayments;

[Area(WePiRemoteServiceConsts.ModuleName)]
[RemoteService(Name = WePiRemoteServiceConsts.RemoteServiceName)]
[Route("api/WePi/pipayment")]
public class PiPaymentController : WePiController
{
    private readonly IPiPaymentAppService _pipaymentAppService;

    public PiPaymentController(IPiPaymentAppService pipaymentAppService)
    {
        _pipaymentAppService = pipaymentAppService;
    }

    [HttpGet]
    public async Task<PiPaymentDto> GetAsync()
    {
        return await _pipaymentAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<PiPaymentDto> GetAuthorizedAsync()
    {
        return await _pipaymentAppService.GetAsync();
    }
}
