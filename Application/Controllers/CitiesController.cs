using Application.Services.Contracts;
using Application.Services.QueryServices.QueryContracts;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityQueryService _cityQueryService;

    public CitiesController(ICityQueryService cityQueryService)
    {
        _cityQueryService = cityQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities(CancellationToken cancellationToken)
    {
        var cities = await _cityQueryService.GetAll(cancellationToken);

        return Ok(cities);
    }
}