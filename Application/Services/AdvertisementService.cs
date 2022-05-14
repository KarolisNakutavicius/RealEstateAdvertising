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
    private readonly IFilterService _filterService;

    public AdvertisementService(IContextService contextService, IRepository<Advertisement> adRepository,
        IRepository<City> cityRepository, IFilterService filterDownService)
    {
        _contextService = contextService;
        _advertisementRepository = adRepository;
        _cityRepository = cityRepository;
        _filterService = filterDownService;
    }

    public async Task<Result<AdvertisementResponse>> CreateNewAdvertisement(CreateAdvertisementRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _contextService.GetCurrentUser();

            var city = await _cityRepository.GetAll(c => c.Name.ToLower() == request.City.ToLower())
                .FirstOrDefaultAsync(cancellationToken);

            if (city == null) city = City.CreateNew(request.City);

            var address = Address.CreateNew(request.Street, request.Number, city, request.Zip);
            var building = Building.CreateNew(address, request.Type, request.Size);


            var file = request.Files.First();
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageBytes = ms.ToArray();

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

    public async Task<IList<AdvertisementResponse>> GetAllUsersAdvertisements(CancellationToken cancellationToken)
    {
        var user = await _contextService.GetCurrentUser();

        var advertisements = await _advertisementRepository.GetAll(a => a.Owner.Id == user.Id, true)
            .Include(a => a.Building)
            .ThenInclude(b => b.Address.City)
            .Select(c => c.ToResponse())
            .ToListAsync(cancellationToken);

        return advertisements;
    }

    public async Task<Result<IList<AdvertisementResponse>>> GetAll(FilterRequest request,
        CancellationToken cancellationToken)
    {
        User user = null;

        try
        {
            user = await _contextService.GetCurrentUser();
        }
        catch (AuthenticationException)
        {
        }

        var advertisements = _advertisementRepository.GetAll(a => user == null || a.Owner.Id != user.Id, true);

        var filterResult = _filterService.FilterDown(advertisements, request);

        if (!filterResult.Success)
            return Result<IList<AdvertisementResponse>>.Fail(filterResult.Errors.Select(e => e.Error).ToList());

        advertisements = filterResult.Data ?? advertisements;

        var result = await advertisements.Include(a => a.Building)
            .ThenInclude(b => b.Address.City)
            .Include(a => a.Owner)
            .Select(c => c.ToResponse())
            .ToListAsync(cancellationToken);

        return Result<IList<AdvertisementResponse>>.Ok(result);
    }
}