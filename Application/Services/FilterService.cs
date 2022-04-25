using Application.DTOs;
using Application.DTOs.InputModels;
using Application.Services.Contracts;
using Domain.Entities;

namespace Application.Services
{
    internal class FilterService : IFilterService
    {
        public Result<IQueryable<Advertisement>> FilterDown(IQueryable<Advertisement> advertisements, FilterRequest request)
        {
            if (request.MinPrice != null && request.MaxPrice != null)
            {
                if (request.MinPrice > request.MaxPrice)
                {
                    return Result<IQueryable<Advertisement>>.Fail("Min price cannot be higher than max price");
                }

                advertisements = advertisements.Where(a => a.Price <= request.MaxPrice && a.Price >= request.MinPrice);
            }

            if (request.CityId != null)
            {
                advertisements = advertisements.Where(a => a.Building.Address.City.Id == request.CityId);
            }

            return Result<IQueryable<Advertisement>>.Ok(advertisements);
        }
    }
}
