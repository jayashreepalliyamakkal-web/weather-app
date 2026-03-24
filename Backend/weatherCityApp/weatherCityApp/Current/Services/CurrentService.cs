using System.Text.Json;

namespace weatherCityApp.Current.Services
{
    // Service responsible for fetching current weather from external API 
    public class CurrentService
    {
        private readonly HttpClient _httpClient;          // Used to make HTTP requests
        private readonly ILogger<CurrentService> _logger; // Logger to track operations & errors

        // Constructor with dependency injection
        public CurrentService(HttpClient httpClient, ILogger<CurrentService> logger)
        {
            _httpClient = httpClient;  // inject HttpClient
            _logger = logger;          // inject logger
        }

        //  Method to fetch current weather for a given city
        public async Task<JsonElement> GetCurrentWeatherAsync(string city)
        {
            // Log the start of the operation
            _logger.LogInformation("Fetching current weather for: {City}", city);

            // Build API URL safely (handles spaces/special characters)
            var url = $"/current.json?q={Uri.EscapeDataString(city)}";

            // Make GET request to external API and read as string
            var response = await _httpClient.GetStringAsync(url);

            // Parse the JSON string into JsonElement (clone so caller can use it independently)
            return JsonDocument.Parse(response).RootElement.Clone();
        }
    }
}