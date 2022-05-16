using Application.DTOs;
using Application.DTOs.ViewModels;
using Application.Enums;

namespace Application.Helpers;

public static class SortingHelper
{
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