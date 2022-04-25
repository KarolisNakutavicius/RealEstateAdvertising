using Application.Services.Contracts;
using Domain.Entities;
using Domain.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CityService : ICityService
    {    
        private readonly IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<City>> GetAll(CancellationToken cancellationToken)
        {
            return await _cityRepository.GetAll(true).ToListAsync(cancellationToken);
        }
    }
}
