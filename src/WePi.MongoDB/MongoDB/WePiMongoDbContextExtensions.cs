using Volo.Abp;
using Volo.Abp.MongoDB;

namespace WePi.MongoDB;

public static class WePiMongoDbContextExtensions
{
    public static void ConfigureWePi(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
