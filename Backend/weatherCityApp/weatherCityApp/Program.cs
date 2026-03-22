using weatherCityApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Register service
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React dev URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();


app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
