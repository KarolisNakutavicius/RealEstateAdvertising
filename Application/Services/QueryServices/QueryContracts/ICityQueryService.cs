using Domain.Entities;

namespace Application.Services.QueryServices.QueryContracts;

public interface ICityQueryService
{
    Task<List<City>> GetAll(CancellationToken cancellationToken);
}