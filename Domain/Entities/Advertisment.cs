using Domain.Entities.Contracts;

namespace Domain.Entities
{
    public class Advertisment : IAggregateRoot
    {
        public int Id { get; set; }

        public Building Building { get; private set; }

        public int OwnerUserId { get; set; }

        public static Advertisment CreateNew(int ownerId)
        {
            return new Advertisment
            {
                OwnerUserId = ownerId,
                Building = Building.CreateNew()
            };
        }
    }
}
