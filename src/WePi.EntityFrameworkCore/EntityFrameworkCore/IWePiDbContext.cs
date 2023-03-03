using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace WePi.EntityFrameworkCore;

[ConnectionStringName(WePiDbProperties.ConnectionStringName)]
public interface IWePiDbContext : IEfCoreDbContext
{
}
