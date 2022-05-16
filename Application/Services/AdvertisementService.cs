using System.Security.Authentication;
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
    private readonly IRepository<City> _cityRepository;
    private readonly IContextService _contextService;
    
    public AdvertisementService(IContextService contextService, IRepository<Advertisement> adRepository,
        IRepository<City> cityRepository)
    {
        _contextService = contextService;
        _advertisementRepository = adRepository;
        _cityRepository = cityRepository;
    }

    public async Task<Result<AdvertisementResponse>> CreateNewAdvertisement(CreateAdvertisementRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _contextService.GetCurrentUserAsync();

            var city = await _cityRepository.GetAll(c => c.Name.ToLower() == request.City.ToLower(), false)
                .FirstOrDefaultAsync(cancellationToken) ?? City.CreateNew(request.City);

            // Open question. According to DDD should I create those in application layer or should they be created in domain layer
            // because I shouldn't be allowed to access those entities not from aggregateRoot
            var address = Address.CreateNew(request.Street, request.Number, city, request.Zip);
            var size = Size.CreateNew(request.BuildingSize, request.PlotSize);
            var building = Building.CreateNew(address, request.Type, size);

            //TODO: import multiple files to db functionality
            var file = request.Files.FirstOrDefault();
            
            byte[]? imageBytes = null;
            
            if (file != null)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms, cancellationToken);
                imageBytes = ms.ToArray();
            }
            
            var advertisement = Advertisement.CreateNew(user, building, imageBytes, request.Name, request.IsRent,
                request.Price, request.Description);

            var result = await _advertisementRepository.Save(advertisement, cancellationToken);

            return Result<AdvertisementResponse>.Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            return Result<AdvertisementResponse>.Fail(e.Message);
        }
    }
}