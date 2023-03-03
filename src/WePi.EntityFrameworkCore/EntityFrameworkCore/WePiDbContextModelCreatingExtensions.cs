using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using WePi.Domain;

namespace WePi.EntityFrameworkCore;

public static class WePiDbContextModelCreatingExtensions
{
    public static void ConfigureWePi(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<PiPayment>(b =>
        {
            //Configure table & schema name
            //b.ToTable("pipayment");
            b.ConfigureByConvention();

        });

    }
}
