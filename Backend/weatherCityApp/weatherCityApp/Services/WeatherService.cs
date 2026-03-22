using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace weatherCityApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = configuration["WeatherApi:Key"]; // Store API key in appsettings.json or environment
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
        }

        public async Task<object> GetCityInfo(string city)
        {
            try
            {
                _logger.LogInformation("Fetching weather info for city: {City}", city);

                // API URLs
                //var weatherUrl = $"https://weatherapi-com.p.rapidapi.com/current.json?q={city}";
                //var timezoneUrl = $"https://weatherapi-com.p.rapidapi.com/timezone.json?q={city}";
                //var astronomyUrl = $"https://weatherapi-com.p.rapidapi.com/astronomy.json?q={city}";

                var weatherUrl = $"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}&aqi=no";
                var astronomyUrl = $"https://api.weatherapi.com/v1/astronomy.json?key={_apiKey}&q={city}";
                var timezoneUrl = $"https://api.weatherapi.com/v1/timezone.json?key={_apiKey}&q={city}";

                // Call APIs
                var weatherResponse = await _httpClient.GetStringAsync(weatherUrl);
                var timezoneResponse = await _httpClient.GetStringAsync(timezoneUrl);
                var astronomyResponse = await _httpClient.GetStringAsync(astronomyUrl);

                // Parse JSON
                var weatherData = JsonDocument.Parse(weatherResponse);
                var timezoneData = JsonDocument.Parse(timezoneResponse);
                var astronomyData = JsonDocument.Parse(astronomyResponse);

                var result = new
                {
                    City = city,
                    Weather = new
                    {
                        TempC = weatherData.RootElement.GetProperty("current").GetProperty("temp_c").GetDouble(),
                        Condition = new
                        {
                            Text = weatherData.RootElement.GetProperty("current").GetProperty("condition").GetProperty("text").GetString()
                        }
                    },
                    Timezone = timezoneData.RootElement.GetProperty("location").GetProperty("tz_id").GetString(),
                    Astronomy = new
                    {
                        Sunrise = astronomyData.RootElement.GetProperty("astronomy").GetProperty("astro").GetProperty("sunrise").GetString(),
                        Sunset = astronomyData.RootElement.GetProperty("astronomy").GetProperty("astro").GetProperty("sunset").GetString()
                    }
                };

                _logger.LogInformation("Successfully fetched info for city: {City}", city);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching weather info for city: {City}", city);
                throw; // Let the controller handle returning the proper response
            }
        }
    }
}