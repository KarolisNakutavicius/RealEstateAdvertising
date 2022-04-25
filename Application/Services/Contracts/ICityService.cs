using Domain.Entities;

namespace Application.Services.Contracts
{
    public interface ICityService
    {
        Task<List<City>> GetAll(CancellationToken cancellationToken);
    }
}
