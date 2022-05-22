using Domain.Entities.Contracts;
using Domain.Entities.JoinedEntities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser, IAggregateRoot
{
    public IList<UserSavedAdvertisement> Advertisements { get; set; }
}