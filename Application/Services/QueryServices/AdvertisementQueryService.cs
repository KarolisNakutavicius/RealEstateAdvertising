using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
using Application.Extensions.Response;
using Application.Helpers;
using Application.Services.Contracts;
using Application.Services.QueryServices.QueryContracts;
using Domain.Entities;
using Domain.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.QueryServices;

internal class AdvertisementQueryService : IAdvertisementQueryService
{
    private readonly IRepository<Advertisement> _advertisementRepository;
    private readonly IContextService _contextService;
    private readonly IFilterService _filterService;

    public AdvertisementQueryService(IContextService contextService, IRepository<Advertisement> adRepository, IFilterService filterDownService)
    {
        _contextService = contextService;
        _advertisementRepository = adRepository;
        _filterService = filterDownService;
    }

    public async Task<PageDto<AdvertisementResponse>> GetAllUsersAdvertisements(PagingRequest pagingRequest,
        CancellationToken cancellationToken)
    {
        var user = await _contextService.GetCurrentUserAsync();

        var advertisements = _advertisementRepository.GetAll(a => a.Owner.Id == user.Id, true)
            .Include(a => a.Building)
            .ThenInclude(b => b.Address.City);

        return await PagingHelper.AddPaging(advertisements, ad => ad.ToResponse(), pagingRequest);
    }

    public async Task<Result<PageDto<AdvertisementResponse>>> GetAll(FilterRequest request,
        PagingRequest pagingRequest,
        CancellationToken cancellationToken)
    {
        var advertisements = _advertisementRepository.GetAll(true);

        if (_contextService.IsAuthenticated)
        {
            var user = await _contextService.GetCurrentUserAsync();

            advertisements = advertisements.Where(a => a.Owner.Id != user.Id);
        }

        var filterResult = _filterService.FilterDown(advertisements, request);

        if (!filterResult.Success)
        {
            return Result<PageDto<AdvertisementResponse>>.Fail(filterResult.Errors.Select(e => e.Error).ToList());
        }
        
        advertisements = filterResult.Data ?? advertisements;

        var result = advertisements.Include(a => a.Building)
            .ThenInclude(b => b.Address.City)
            .Include(a => a.Owner);
        
        var pagedResult = await PagingHelper.AddPaging(advertisements, ad => ad.ToResponse(), pagingRequest);

        return Result<PageDto<AdvertisementResponse>>.Ok(pagedResult);
    }
}