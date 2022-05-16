using Application.DTOs;
using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
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
    [ProducesResponseType(typeof(PageDto<AdvertisementResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserAds([FromQuery] PagingRequest paging, CancellationToken cancellationToken)
    {
        var result = await _queryService.GetAllUsersAdvertisements(paging, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PageDto<AdvertisementResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAds([FromQuery] FilterRequest request, [FromQuery] PagingRequest paging, CancellationToken cancellationToken)
    {
        var result = await _queryService.GetAll(request, paging, cancellationToken);

        return result.ToHttpResponse();
    }
}