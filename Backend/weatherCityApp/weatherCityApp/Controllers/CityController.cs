using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using weatherCityApp.Services;

[ApiController]
[Route("api")]
public class CityController : ControllerBase
{
    private readonly WeatherService _weatherService;
    private readonly ILogger<CityController> _logger;

    public CityController(WeatherService weatherService, ILogger<CityController> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [HttpGet("city-info/{city}")]
    public async Task<IActionResult> GetCityInfo(string city)
    {
        _logger.LogInformation("Received request for city info: {City}", city);

        try
        {
            var data = await _weatherService.GetCityInfo(city);
            _logger.LogInformation("Returning data for city: {City}", city);
            return Ok(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get city info for: {City}", city);
            return StatusCode(500, "An error occurred while retrieving city info.");
        }
    }
}