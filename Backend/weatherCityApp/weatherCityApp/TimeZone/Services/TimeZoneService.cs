using System.Text.Json;

namespace weatherCityApp.TimeZone.Services
{
    // Service responsible for fetching timezone data from external API 
    public class TimezoneService
    {
        private readonly HttpClient _httpClient;         // To make HTTP requests
        private readonly ILogger<TimezoneService> _logger; // To log actions/errors

        // Constructor uses Dependency Injection 
        public TimezoneService(HttpClient httpClient, ILogger<TimezoneService> logger)
        {
            _httpClient = httpClient;  // inject HttpClient
            _logger = logger;          // inject logger
        }

        // Method to fetch timezone info for a given city 
        public async Task<JsonElement> GetTimezoneAsync(string city)
        {
            // Log the start of the operation
            _logger.LogInformation("Fetching timezone for: {City}", city);

            // Build API URL safely using URL encoding
            var url = $"/timezone.json?q={Uri.EscapeDataString(city)}";

            // Make GET request and get raw JSON as string
            var response = await _httpClient.GetStringAsync(url);

            // Parse string response into JSON object and return
            return JsonDocument.Parse(response).RootElement.Clone();
        }
    }
}