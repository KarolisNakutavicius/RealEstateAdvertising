using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
using Application.Extensions.Response;
using Application.Services.Contracts;
using Domain.Entities;
using Domain.Services.Contracts;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

internal class AdvertisementService : IAdvertisementService
{
    private readonly IRepository<Advertisement> _advertisementRepository;
    private readonly IContextService _contextService;

    public AdvertisementService(IContextService contextService, IRepository<Advertisement> adRepository)
    {
        _contextService = contextService;
        _advertisementRepository = adRepository;
    }

    public async Task<Result<AdvertisementResponse>> CreateNewAdvertisement(CreateAdvertisementRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _contextService.GetCurrentUser();

            var address = Address.CreateNew(request.Street, request.Number, request.City, request.Zip);
            var building = Building.CreateNew(address, request.Type, request.Size);
            var advertisement = Advertisement.CreateNew(user, building, request.Name, request.IsRent, request.Price, request.Description);

            var result = await _advertisementRepository.Save(advertisement, cancellationToken);

            return Result<AdvertisementResponse>.Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            return Result<AdvertisementResponse>.Fail(e.Message);
        }
    }

    public async Task<IList<AdvertisementResponse>> GetAllUsersAdvertisements(CancellationToken cancellationToken)
    {
        var user = await _contextService.GetCurrentUser();

        var advertisements = await _advertisementRepository.GetAll(a => a.Owner.Id == user.Id)
            .Include(a => a.Building)
            .Select(c => c.ToResponse())
            .ToListAsync(cancellationToken);

        return advertisements;
    }
}