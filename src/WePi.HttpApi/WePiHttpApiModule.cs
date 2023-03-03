using Localization.Resources.AbpUi;
using WePi.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using WePi.Domain;

namespace WePi;
[DependsOn(
    typeof(WePiApplicationContractsModule),
    typeof(WePiApplicationModule),
    typeof(AbpAspNetCoreMvcModule))
    ]
public class WePiHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WePiHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WePiResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
        context.Services.AddTransient<PiPaymentManager>();
    }
}
