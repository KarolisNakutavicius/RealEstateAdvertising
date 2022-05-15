using Application.DTOs.InputModels;
using Application.Extensions;
using Application.Services.Contracts;
using Application.Services.QueryServices.QueryContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Advertisements : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IAdvertisementQueryService _queryService;

    public Advertisements(IAdvertisementService advertisementService,
        IAdvertisementQueryService queryService)
    {
        _advertisementService = advertisementService;
        _queryService = queryService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] CreateAdvertisementRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _advertisementService.CreateNewAdvertisement(request, cancellationToken);

        return result.ToHttpResponse();
    }

    [HttpGet("mine")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserAds(CancellationToken cancellationToken)
    {
        var result = await _queryService.GetAllUsersAdvertisements(cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAds([FromQuery] FilterRequest request, CancellationToken cancellationToken)
    {
        var result = await _queryService.GetAll(request, cancellationToken);

        return result.ToHttpResponse();
    }
}