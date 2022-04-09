using Domain.Entities.Contracts;

namespace Domain.Entities
{
    public class Advertisment : IAggregateRoot
    {

        protected Advertisment()
        {
            // constructor when materializing from db
        }

        public Building Building { get; private set; }

        public static Advertisment CreateNew()
        {
            return new Advertisment
            {
                Building = Building.CreateNew()
            };
        }
    }
}
