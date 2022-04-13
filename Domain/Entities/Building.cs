using Domain.ValueObjects;

namespace Domain.Entities;

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