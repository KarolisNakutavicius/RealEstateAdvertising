using Domain.Entities.Contracts;

namespace Domain.Entities
{
    public class User : IAggregateRoot
    {
        public int Id { get; set; }

        public IList<int> SavedAdsIds { get; set; } = new List<int>();

        public void SaveAdvertisment(int advertismentId)
        {
            SavedAdsIds.Add(advertismentId);
        }
    }
}
