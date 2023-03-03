using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using WePi.Domain;

namespace WePi.EntityFrameworkCore;

[ConnectionStringName(WePiDbProperties.ConnectionStringName)]
public class WePiDbContext : AbpDbContext<WePiDbContext>, IWePiDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<PiPayment> PiPayments { get; set; }

    public WePiDbContext(DbContextOptions<WePiDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureWePi();
    }
}
