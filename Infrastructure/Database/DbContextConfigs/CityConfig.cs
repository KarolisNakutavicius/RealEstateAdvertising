using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.DbContextConfigs;

internal static class CityConfig
{
    internal static void BuildModel(this EntityTypeBuilder<City> builder)
    {
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

        builder.HasKey(u => u.Id);
    }
}