using Domain.Entities.Contracts;

namespace Domain.Entities;

public class Advertisement : IAggregateRoot
{
    public int Id { get; set; }

    public Building Building { get; private set; }

    public User Owner { get; set; }

    public static Advertisement CreateNew(User owner)
    {
        return new Advertisement
        {
            Owner = owner,
            Building = Building.CreateNew()
        };
    }
}