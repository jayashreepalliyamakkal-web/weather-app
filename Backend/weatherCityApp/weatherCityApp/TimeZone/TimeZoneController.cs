using Microsoft.AspNetCore.Mvc;
using weatherCityApp.TimeZone.Services;

namespace weatherCityApp.Controllers
{
    [ApiController]
    [Route("api/timezone")]
    public class TimezoneController : ControllerBase
    {
        private readonly TimezoneService _service;
        private readonly ILogger<TimezoneController> _logger;

        // Constructor with DI for service + logger
        public TimezoneController(TimezoneService service, ILogger<TimezoneController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>GET /api/timezone/{city}</summary>
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                _logger.LogWarning("Empty city parameter in timezone request."); 
                return BadRequest(new { error = "City parameter is required." });
            }

            try
            {
                _logger.LogInformation("Fetching timezone data for city: {City}", city);

                var data = await _service.GetTimezoneAsync(city);

                _logger.LogInformation("Successfully fetched timezone data for city: {City}", city);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching timezone data for city: {City}", city);
                return StatusCode(500, new { error = "Internal server error." });
            }
        }
    }
}