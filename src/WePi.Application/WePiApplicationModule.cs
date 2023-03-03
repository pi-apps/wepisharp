using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Http.Client;

namespace WePi;
[DependsOn(
    typeof(WePiDomainModule),
    typeof(WePiApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpHttpClientModule),
    typeof(AbpAutoMapperModule)
    )]
public class WePiApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<WePiApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WePiApplicationModule>(validate: true);
        });
    }
}