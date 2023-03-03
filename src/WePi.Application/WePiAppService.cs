using WePi.Localization;
using Volo.Abp.Application.Services;

namespace WePi;

public abstract class WePiAppService : ApplicationService
{
    protected WePiAppService()
    {
        LocalizationResource = typeof(WePiResource);
        ObjectMapperContext = typeof(WePiApplicationModule);
    }
}
