using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
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
}
