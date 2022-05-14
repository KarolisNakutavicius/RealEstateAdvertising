using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;

public class RealEstateAdvertisingContextFactory : IDesignTimeDbContextFactory<RealEstateAdvertisingDbContext>
{
    public RealEstateAdvertisingDbContext CreateDbContext(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Application"))
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<RealEstateAdvertisingDbContext>();
        var connectionString = config.GetConnectionString("RealEstateAdvertisingDbContext");

        optionsBuilder.UseSqlServer(connectionString);

        return new RealEstateAdvertisingDbContext(optionsBuilder.Options);
    }
}