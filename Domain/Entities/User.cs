using Domain.Entities.Contracts;

namespace Domain.Entities
{
    internal class User : IAggregateRoot
    {
        public IList<int> SavedAdsIds { get; set; } = new List<int>();

        public void SaveAdvertisment(int advertismentId)
        {
            SavedAdsIds.Add(advertismentId);
        }
    }
}
