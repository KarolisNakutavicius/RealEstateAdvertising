using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.DbContextConfigs
{
    internal static class BuildingConfig
    {
        internal static void BuildModel(this EntityTypeBuilder<Building> builder)
        {
            builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Address);
        }
    }
}
