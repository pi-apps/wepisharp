using WePi.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace WePi;

public abstract class WePiController : AbpControllerBase
{
    protected WePiController()
    {
        LocalizationResource = typeof(WePiResource);
    }
}
