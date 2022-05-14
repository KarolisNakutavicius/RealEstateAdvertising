using Domain.Entities.Contracts;

namespace Domain.Entities;

public class City : IAggregateRoot
{
    public int Id { get; set; }

    public string Name { get; init; } = string.Empty;


    public static City CreateNew(string name)
    {
        return new City
        {
            Name = name
        };
    }
}