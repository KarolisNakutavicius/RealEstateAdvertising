using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;

namespace Application.Services.QueryServices.QueryContracts;

public interface IAdvertisementQueryService
{
    Task<PageDto<AdvertisementResponse>> GetAllUsersAdvertisements(PagingRequest pagingRequest,
        CancellationToken cancellationToken);

    Task<Result<PageDto<AdvertisementResponse>>> GetAll(FilterRequest request,
        PagingRequest pagingRequest,
        CancellationToken cancellationToken);

    Task<PageDto<AdvertisementResponse>> GetSavedAdvertisements(PagingRequest paging, CancellationToken cancellationToken);
}