using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.DbContextConfigs
{
    internal static class BuildingCategoryConfig
    {
        internal static void BuildModel(this EntityTypeBuilder<BuildingCategory> builder)
        {

        }
    }
}
