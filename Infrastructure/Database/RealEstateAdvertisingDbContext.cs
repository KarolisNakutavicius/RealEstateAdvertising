using Domain.Entities;
using Infrastructure.Database.DbContextConfigs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class RealEstateAdvertisingDbContext : IdentityDbContext<User>
    {
        public RealEstateAdvertisingDbContext(DbContextOptions<RealEstateAdvertisingDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<BuildingCategory> BuildingCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Advertisement>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<Building>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<BuildingCategory>(entityBuilder => entityBuilder.BuildModel());
        }
    }
}
