using Microsoft.AspNetCore.Mvc;
using weatherCityApp.Current.Services;

namespace weatherCityApp.Controllers
{
    // API Controller for "current weather" endpoints 
    [ApiController] // Enables automatic model validation and API behavior
    [Route("api/current")] // Base route for this controller
    public class CurrentController : ControllerBase
    {
        private readonly CurrentService _service; // Service layer for fetching current weather
        private readonly ILogger<CurrentController> _logger;
        
        
        // Constructor with DI for service + logger
        public CurrentController(CurrentService service, ILogger<CurrentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// GET /api/current/{city}
        /// Fetch current weather for a specific city
        /// </summary>
        [HttpGet("{city}")] // Route: /api/current/dublin
        public async Task<IActionResult> Get(string city)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest(new { error = "City parameter is required." });

            try
            {
                // Call service to fetch current weather
                var data = await _service.GetCurrentWeatherAsync(city);

                _logger.LogInformation("Successfully fetched current weather for city: {City}", city);

                // Return HTTP 200 OK with the JSON payload
                return Ok(data);
            }
            catch (Exception ex)
            {
                // log error with exception details
                _logger.LogError(ex, "Error fetching current weather for city: {City}", city);
                return StatusCode(500, new { error = "Internal server error." });
            }
        }
    }
}