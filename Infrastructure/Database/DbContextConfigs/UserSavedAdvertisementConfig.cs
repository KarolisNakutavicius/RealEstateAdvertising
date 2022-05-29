using Domain.Entities.JoinedEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.DbContextConfigs;

internal static class UserSavedAdvertisementConfig
{
    internal static void BuildModel(this EntityTypeBuilder<UserSavedAdvertisement> builder)
    {
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("int");

        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.User)
            .WithMany(u => u.Advertisements)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Advertisement)
            .WithMany();
    }
}