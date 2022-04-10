using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.DbContextConfigs
{
    internal static class UserConfig
    {
        internal static void BuildModel(this EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            builder.HasKey(u => u.Id);
        }
    }
}
