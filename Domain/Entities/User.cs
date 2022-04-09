using Domain.Entities.Contracts;

namespace Domain.Entities
{
    internal class User : IAggregateRoot
    {
        public IList<Advertisment> SavedAdvertisments { get; set; } = new List<Advertisment>();

        public IList<Advertisment> PublishedAdvertisments { get; set; } = new List<Advertisment>();

        public void SaveAdvertisment(Advertisment advertisment)
        {
            SavedAdvertisments.Add(advertisment);
        }

        public void Publish(Advertisment advertisment)
        {
            PublishedAdvertisments.Add(advertisment);
        }
    }
}
