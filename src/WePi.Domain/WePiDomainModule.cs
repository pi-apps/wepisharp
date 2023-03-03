using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace WePi;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(WePiDomainSharedModule)
)]
public class WePiDomainModule : AbpModule
{

}
