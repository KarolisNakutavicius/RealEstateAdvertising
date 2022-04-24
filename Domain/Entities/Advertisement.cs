using Domain.Entities.Contracts;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Advertisement : IAggregateRoot
{
    public int Id { get; init; }

    public bool IsRent { get; init; }

    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public Building Building { get; private init; } = new Building();

    public User Owner { get; set; } = new User();

    public byte[] Image { get; set; }

    public static Advertisement CreateNew(User owner, Building building, byte[] image, string title, bool isRent, decimal price, string description)
    {
        return new Advertisement
        {
            Image = image,
            Title = title,
            Owner = owner,
            Building = building,
            Price = price,
            IsRent = isRent,
            Description = description
        };
    }
}