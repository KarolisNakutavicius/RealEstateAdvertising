using System.Drawing;
using Domain.Enums;
using Domain.ValueObjects;
using Size = Domain.ValueObjects.Size;

namespace Domain.Entities;

public class Building
{
    public int Id { get; init; }

    public BuildingType Category { get; init; }

    public Size Size { get; set; } = new();

    public Address Address { get; init; } = new();

    public static Building CreateNew(Address address, BuildingType category, Size size)
    {
        return new Building
        {
            Address = address,
            Category = category,
            Size = size
        };
    }
}