using Application.DTOs;
using Application.DTOs.ViewModels;
using Application.Enums;
using Domain.Entities;
using Domain.Enums;

namespace Application.Extensions.Response;

internal static class AdvertisementResponseExtensions
{
    public static AdvertisementResponse ToResponse(this Advertisement advertisement)
    {
        return new AdvertisementResponse
        {
            Id = advertisement.Id,
            City = advertisement.Building.Address.City.Name,
            Description = advertisement.Description,
            Name = advertisement.Title,
            IsRent = advertisement.IsRent,
            Number = advertisement.Building.Address.Number,
            BuildingSize = advertisement.Building.Size.BuildingSize,
            PlotSize = advertisement.Building.Size.PlotSize,
            Type = Enum.GetName(typeof(BuildingType), advertisement.Building.Category) ?? string.Empty,
            Zip = advertisement.Building.Address.Zip,
            Price = advertisement.Price,
            Image = advertisement.Image,
            Street = advertisement.Building.Address.Street,
            OwnerEmail = advertisement.Owner.Email
        };
    }
    
    public static PageDto<AdvertisementResponse> SortAds(this PageDto<AdvertisementResponse> ads, SortBy sortBy)
    {
        return sortBy switch
        {
            SortBy.None => ads,
            SortBy.Price => ads.ConvertItems(ads.Items.OrderBy(i => i.Price).ToList()),
            SortBy.Size => ads.ConvertItems(ads.Items.OrderBy(i => i.BuildingSize + i.PlotSize).ToList()),
            _ => ads
        };
    }

    private static PageDto<AdvertisementResponse> ConvertItems(this PageDto<AdvertisementResponse> ads, IEnumerable<AdvertisementResponse> orderedItems)
    {
        return new PageDto<AdvertisementResponse>()
        {
            Items = orderedItems,
            PageSize = ads.PageSize,
            CurrentPage = ads.CurrentPage,
            TotalPages = ads.TotalPages,
            TotalRecordsCount = ads.TotalRecordsCount
        };
    }
}