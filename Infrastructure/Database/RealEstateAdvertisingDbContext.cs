using Domain.Entities;
using Infrastructure.Database.DbContextConfigs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class RealEstateAdvertisingDbContext : DbContext
    {
        public RealEstateAdvertisingDbContext(DbContextOptions<RealEstateAdvertisingDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Advertisement> Advertisments { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<BuildingCategory> BuildingCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<Advertisement>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<Building>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<BuildingCategory>(entityBuilder => entityBuilder.BuildModel());
        }
    }
}
