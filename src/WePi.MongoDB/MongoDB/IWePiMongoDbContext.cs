using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace WePi.MongoDB;

[ConnectionStringName(WePiDbProperties.ConnectionStringName)]
public interface IWePiMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
