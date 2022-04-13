using Domain.Entities.Contracts;

namespace Domain.Entities;

public class Advertisement : IAggregateRoot
{
    public int Id { get; set; }

    public Building Building { get; private set; }

    public int OwnerUserId { get; set; }

    public static Advertisement CreateNew(int ownerId)
    {
        return new Advertisement
        {
            OwnerUserId = ownerId,
            Building = Building.CreateNew()
        };
    }
}