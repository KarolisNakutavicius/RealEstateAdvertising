using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Building
{
    public int Id { get; init; }

    public BuildingType Category { get; init; }

    public int Size { get; init; }

    public Address Address { get; init; } = new Address();

    public static Building CreateNew(Address address, BuildingType category, int size)
    {
        return new Building()
        {
            Address = address,
            Category = category,
            Size = size
            
        };
    }
}