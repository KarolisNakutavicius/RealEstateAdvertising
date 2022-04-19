using Application.DTOs.ViewModels;
using Domain.Entities;

namespace Application.Extensions.Response
{
    internal static class AdvertisementResponseExtensions
    {
        public static AdvertisementResponse ToResponse(this Advertisement advertisement)
        {
            return new AdvertisementResponse()
            {
                Id = advertisement.Id,
                City = advertisement.Building.Address.City,
                Description = advertisement.Description,
                Name = advertisement.Title,
                IsRent = advertisement.IsRent,
                Number = advertisement.Building.Address.Number,
                Size = advertisement.Building.Size,
                Type = advertisement.Building.Category,
                Zip = advertisement.Building.Address.Zip,
                Price = advertisement.Price
            };
        }
    }
}
