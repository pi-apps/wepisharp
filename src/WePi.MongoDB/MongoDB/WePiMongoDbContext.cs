using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace WePi.MongoDB;

[ConnectionStringName(WePiDbProperties.ConnectionStringName)]
public class WePiMongoDbContext : AbpMongoDbContext, IWePiMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureWePi();
    }
}
