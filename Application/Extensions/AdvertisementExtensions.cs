using Application.DTOs;
using Application.DTOs.ViewModels;
using Application.Enums;
using Application.Extensions.Response;
using Domain.Entities;

namespace Application.Extensions;

public static class AdvertisementExtensions
{
    public static IQueryable<Advertisement> SortAds(this IQueryable<Advertisement> ads, SortBy sortBy)
    {
        return sortBy switch
        {
            SortBy.None => ads,
            SortBy.Price => ads.OrderBy(a => a.Price),
            SortBy.Size => ads.OrderByDescending(i => i.Building.Size.BuildingSize + i.Building.Size.PlotSize),
            _ => ads
        };
    }
}