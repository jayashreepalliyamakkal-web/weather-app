using System.Text.Json;

namespace weatherCityApp.Astronomy.Services
{
    //  Service responsible for fetching astronomy data from external API 
    public class AstronomyService
    {
        private readonly HttpClient _httpClient; // used to call external API
        private readonly ILogger<AstronomyService> _logger;  // logging for debugging & monitoring

        //  Constructor with dependency injection 
        public AstronomyService(HttpClient httpClient, ILogger<AstronomyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        //  Method to fetch astronomy data for a given city 
        public async Task<JsonElement> GetAstronomyAsync(string city, string? date = null)
        {
            try
            {
                // Build API URL (encode city to handle spaces/special characters)
                var url = $"/astronomy.json?q={Uri.EscapeDataString(city)}";

                // Make HTTP GET request
                var response = await _httpClient.GetAsync(url);

                // Read raw response content
                var content = await response.Content.ReadAsStringAsync();

            
                //  Throw error if not success (403, 429, etc.)
                response.EnsureSuccessStatusCode();

                // Parse JSON response safely
                var json = JsonDocument.Parse(content).RootElement.Clone();

                return json;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error for {City}", city);
                throw;
            }
        }
    }
}