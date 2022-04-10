using Domain.Entities;
using Infrastructure.Database.DbContextConfigs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    internal class RealEstateAdvertisingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Advertisment> Advertisments { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<BuildingCategory> BuildingCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<Advertisment>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<Building>(entityBuilder => entityBuilder.BuildModel());
            builder.Entity<BuildingCategory>(entityBuilder => entityBuilder.BuildModel());
        }
    }
}
