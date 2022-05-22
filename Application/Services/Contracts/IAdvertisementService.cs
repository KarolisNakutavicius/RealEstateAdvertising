using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;

namespace Application.Services.Contracts;

public interface IAdvertisementService
{
    Task<Result<AdvertisementResponse>> CreateNewAdvertisement(CreateAdvertisementRequest request,
        CancellationToken cancellationToken);

    Task<Result> SaveAdvertisement(SaveAdvertisementRequest request, CancellationToken cancellationToken);
}