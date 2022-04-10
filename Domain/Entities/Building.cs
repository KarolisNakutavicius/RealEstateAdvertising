using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Building
    {
        public int Id { get; set; }
        public BuildingCategory Category { get; private set; }

        public Address Address { get; private set; }

        public static Building CreateNew()
        {
            return new Building();
        }
    }
}
