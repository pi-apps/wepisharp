using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace WePi;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class WePiInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WePiInstallerModule>();
        });
    }
}
