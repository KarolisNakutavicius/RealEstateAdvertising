using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.DbContextConfigs
{
    internal static class BuildingCategoryConfig
    {
        internal static void BuildModel(this EntityTypeBuilder<BuildingCategory> builder)
        {
            builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

            builder.HasKey(u => u.Id);
        }
    }
}
