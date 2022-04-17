using Domain.Entities.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser, IAggregateRoot
{

    //public IList<int> SavedAdsIds { get; set; } = new List<int>();

    //public void SaveAdvertisment(int advertismentId)
    //{
    //    SavedAdsIds.Add(advertismentId);
    //}
}