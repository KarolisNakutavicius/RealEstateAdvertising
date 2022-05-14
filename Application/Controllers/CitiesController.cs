using Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities(CancellationToken cancellationToken)
    {
        var cities = await _cityService.GetAll(cancellationToken);

        return Ok(cities);
    }
}