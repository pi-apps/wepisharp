using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace WePi.EntityFrameworkCore;

[DependsOn(
    typeof(WePiDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class WePiEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WePiDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}
