using TestProject.Common;
using TestProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWeatherService, WeatherService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (IWeatherService weatherService) =>
{
    var forecast = weatherService.GetWeatherForecasts();
    return forecast;
})
.WithName("GetWeatherForecasts");

app.MapGet("/weatherforecast/{id}", (int id, IWeatherService weatherService) =>
{
    var forecast = weatherService.GetWeatherForecast(id);
    return forecast is not null ? Results.Ok(forecast) : Results.NotFound();
})
.WithName("GetWeatherForecastById");

app.MapPost("/weatherforecast", (WeatherForecast forecast, IWeatherService weatherService) =>
{
    weatherService.AddWeatherForecast(forecast);
    return Results.Created($"/weatherforecast/{forecast.Id}", forecast);
})
.WithName("AddWeatherForecast");

app.MapPut("/weatherforecast/{id}", (int id, WeatherForecast updatedForecast, IWeatherService weatherService) =>
{
    weatherService.UpdateWeatherForecast(id, updatedForecast);
    return Results.NoContent();
})
.WithName("UpdateWeatherForecast");

app.MapDelete("/weatherforecast/{id}", (int id, IWeatherService weatherService) =>
{
    weatherService.DeleteWeatherForecast(id);
    return Results.NoContent();
})
.WithName("DeleteWeatherForecast");

app.Run();

public partial class Program { }