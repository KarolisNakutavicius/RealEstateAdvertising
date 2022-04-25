using Application.DTOs;
using Application.DTOs.InputModels;
using Domain.Entities;

namespace Application.Services.Contracts
{
    internal interface IFilterService
    {
        Result<IQueryable<Advertisement>> FilterDown(IQueryable<Advertisement> advertisements, FilterRequest request);
    }
}
