﻿using Application.DTOs;
using Application.DTOs.InputModels;
using Application.Resources;
using Application.Services.Contracts;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

internal class FilterService : IFilterService
{
    public Result<IQueryable<Advertisement>> FilterDown(IQueryable<Advertisement> advertisements, FilterRequest request)
    {
        if (request.MinPrice != null && request.MaxPrice != null)
        {
            if (request.MinPrice > request.MaxPrice)
            {
                return Result<IQueryable<Advertisement>>.Fail(ErrorMessages.MinPriceHigherThanMax);
            }
            
            advertisements = advertisements.Where(a => a.Price <= request.MaxPrice && a.Price >= request.MinPrice);
        }

        if (request.CityId is > 0)
        {
            advertisements = advertisements.Where(a => a.Building.Address.City.Id == request.CityId);
        }

        if (request.Type is > 0)
        {
            advertisements = advertisements.Where(a => a.Building.Category == request.Type);
        }

        if (request.IsRent != null)
        {
            advertisements = advertisements.Where(a => a.IsRent == request.IsRent);
        }
        
        return Result<IQueryable<Advertisement>>.Ok(advertisements);
    }
}