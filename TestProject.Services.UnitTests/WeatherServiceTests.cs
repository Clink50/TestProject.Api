using TestProject.Common;

namespace TestProject.Services.UnitTests;

public class WeatherServiceTest
{
	private readonly WeatherService _weatherService;

	public WeatherServiceTest()
	{
		_weatherService = new WeatherService();
	}

	[Fact]
	public void GetWeatherForecasts_ReturnsEmptyList_WhenNoForecastsAdded()
	{
		var forecasts = _weatherService.GetWeatherForecasts();
		Assert.Empty(forecasts);
	}

	[Fact]
	public void AddWeatherForecast_AddsForecastSuccessfully()
	{
		var forecast = new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" };
		_weatherService.AddWeatherForecast(forecast);

		var forecasts = _weatherService.GetWeatherForecasts();
		Assert.Single(forecasts);
		Assert.Contains(forecast, forecasts);
	}

	[Fact]
	public void GetWeatherForecast_ReturnsCorrectForecast_WhenForecastExists()
	{
		var forecast = new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" };
		_weatherService.AddWeatherForecast(forecast);

		var retrievedForecast = _weatherService.GetWeatherForecast(forecast.Id);
		Assert.NotNull(retrievedForecast);
		Assert.Equal(forecast.Id, retrievedForecast.Id);
	}

	[Fact]
	public void GetWeatherForecast_ReturnsNull_WhenForecastDoesNotExist()
	{
		var forecast = _weatherService.GetWeatherForecast(999);
		Assert.Null(forecast);
	}

	[Fact]
	public void UpdateWeatherForecast_UpdatesForecastSuccessfully()
	{
		var forecast = new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" };
		_weatherService.AddWeatherForecast(forecast);

		var updatedForecast = new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 30, Summary = "Hot" };
		_weatherService.UpdateWeatherForecast(forecast.Id, updatedForecast);

		var retrievedForecast = _weatherService.GetWeatherForecast(forecast.Id);
		Assert.NotNull(retrievedForecast);
		Assert.Equal(updatedForecast.TemperatureC, retrievedForecast.TemperatureC);
		Assert.Equal(updatedForecast.Summary, retrievedForecast.Summary);
	}

	[Fact]
	public void DeleteWeatherForecast_RemovesForecastSuccessfully()
	{
		var forecast = new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" };
		_weatherService.AddWeatherForecast(forecast);

		_weatherService.DeleteWeatherForecast(forecast.Id);

		var retrievedForecast = _weatherService.GetWeatherForecast(forecast.Id);
		Assert.Null(retrievedForecast);
	}
}