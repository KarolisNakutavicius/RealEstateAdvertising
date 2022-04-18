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
    internal static class AdvertismentConfig
    {
        internal static void BuildModel(this EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Building);

            builder.HasOne(u => u.Owner)
                .WithOne();
        }
    }
}
