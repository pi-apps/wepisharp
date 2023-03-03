using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace WePi;

[DependsOn(
    typeof(WePiApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class WePiHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(WePiApplicationContractsModule).Assembly,
            WePiRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WePiHttpApiClientModule>();
        });

    }
}
