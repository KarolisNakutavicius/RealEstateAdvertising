using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;

namespace Application.Services.QueryServices.QueryContracts;

public interface IAdvertisementQueryService
{
    Task<IList<AdvertisementResponse>> GetAllUsersAdvertisements(CancellationToken cancellationToken);

    Task<Result<IList<AdvertisementResponse>>> GetAll(FilterRequest request,
        CancellationToken cancellationToken);
}