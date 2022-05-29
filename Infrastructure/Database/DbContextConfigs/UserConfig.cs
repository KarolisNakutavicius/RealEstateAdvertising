using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.DbContextConfigs;

internal static class UserConfig
{
    internal static void BuildModel(this EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Advertisements)
            .WithOne(a => a.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}