using Application.Services.QueryServices.QueryContracts;
using Domain.Entities;
using Domain.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.QueryServices;

public class CityQueryService : ICityQueryService
{
    private readonly IRepository<City> _cityRepository;

    public CityQueryService(IRepository<City> cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<List<City>> GetAll(CancellationToken cancellationToken)
    {
        return await _cityRepository.GetAll(true).ToListAsync(cancellationToken);
    }
}