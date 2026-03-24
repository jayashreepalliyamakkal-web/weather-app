using weatherCityApp.Astronomy.Services;
using weatherCityApp.Current.Services;
using weatherCityApp.TimeZone.Services;

var builder = WebApplication.CreateBuilder(args);

// Read allowed origins from configuration
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

// ----------------CORS (allow React dev server)--------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
       policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// -------------- Named HttpClient — reads all values from appsettings.json ------------------------------
builder.Services.AddHttpClient("WeatherApi", client =>
{
    var config = builder.Configuration;

    var apiKey = config["WeatherApi:Key"]
                  ?? throw new InvalidOperationException("WeatherApi:Key is not configured.");
    var host = config["WeatherApi:Host"]
                  ?? throw new InvalidOperationException("WeatherApi:Host is not configured.");
    var baseUrl = config["WeatherApi:BaseUrl"]
                  ?? throw new InvalidOperationException("WeatherApi:BaseUrl is not configured.");

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", host);
});

// ---------------------Services----------------------------------------------------
builder.Services.AddScoped<CurrentService>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var logger = sp.GetRequiredService<ILogger<CurrentService>>();
    return new CurrentService(factory.CreateClient("WeatherApi"), logger);
});

builder.Services.AddScoped<TimezoneService>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var logger = sp.GetRequiredService<ILogger<TimezoneService>>();
    return new TimezoneService(factory.CreateClient("WeatherApi"), logger);
});

builder.Services.AddScoped<AstronomyService>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var logger = sp.GetRequiredService<ILogger<AstronomyService>>();
    return new AstronomyService(factory.CreateClient("WeatherApi"), logger);
});


builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("ReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();