using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Common;

namespace TestProject.Api.IntegrationTests;

public class ApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task GetWeatherForecasts_ReturnsSuccessStatusCode()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/weatherforecast");

        response.EnsureSuccessStatusCode();
        var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.NotNull(forecasts);
    }

    [Fact]
    public async Task GetWeatherForecastById_ReturnsNotFound_ForInvalidId()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/weatherforecast/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task AddWeatherForecast_ReturnsCreatedStatusCode()
    {
        var client = _factory.CreateClient();
        var newForecast = new WeatherForecast { Id = 1, Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Sunny" };

        var response = await client.PostAsJsonAsync("/weatherforecast", newForecast);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task UpdateWeatherForecast_ReturnsNoContentStatusCode()
    {
        var client = _factory.CreateClient();
        var updatedForecast = new WeatherForecast { Id = 1, Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 30, Summary = "Hot" };

        var response = await client.PutAsJsonAsync("/weatherforecast/1", updatedForecast);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteWeatherForecast_ReturnsNoContentStatusCode()
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync("/weatherforecast/1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}