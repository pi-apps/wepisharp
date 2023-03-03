using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WePi.EntityFrameworkCore;

public class WePiHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<WePiHttpApiHostMigrationsDbContext>
{
    public WePiHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<WePiHttpApiHostMigrationsDbContext>()
            .UseNpgsql(configuration.GetConnectionString("WePi"))
            .UseSnakeCaseNamingConvention();

        return new WePiHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
