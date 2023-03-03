using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace WePi.EntityFrameworkCore;

public class WePiHttpApiHostMigrationsDbContext : AbpDbContext<WePiHttpApiHostMigrationsDbContext>
{
    public WePiHttpApiHostMigrationsDbContext(DbContextOptions<WePiHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureWePi();
    }
}
