using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace WePi.PiPayments;

public interface IPiPaymentAppService : IApplicationService
{
    Task<PiPaymentDto> GetAsync();

    Task<PiPaymentDto> GetAuthorizedAsync();
}
