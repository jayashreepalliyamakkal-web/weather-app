using Microsoft.AspNetCore.Mvc;
using weatherCityApp.Astronomy.Services;

namespace weatherCityApp.Controllers
{
    [ApiController]
    [Route("api/astronomy")]
    public class AstronomyController : ControllerBase
    {
        private readonly AstronomyService _service;
        private readonly ILogger<AstronomyController> _logger; 

        // Constructor with DI for service + logger
        public AstronomyController(AstronomyService service, ILogger<AstronomyController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>GET /api/astronomy/{city}</summary>
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                _logger.LogWarning("Empty city parameter in astronomy request."); 
                return BadRequest(new { error = "City parameter is required." });
            }

            try
            {
                _logger.LogInformation("Fetching astronomy data for city: {City}", city);

                var data = await _service.GetAstronomyAsync(city);

                _logger.LogInformation("Successfully fetched astronomy data for city: {City}", city);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching astronomy data for city: {City}", city);
                return StatusCode(500, new { error = "Internal server error." });
            }
        }
    }
}