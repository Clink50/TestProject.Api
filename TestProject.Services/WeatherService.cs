using TestProject.Common;
using System.Collections.Concurrent;

namespace TestProject.Services;

public interface IWeatherService
{
	IEnumerable<WeatherForecast> GetWeatherForecasts();
	WeatherForecast? GetWeatherForecast(int id);
	void AddWeatherForecast(WeatherForecast forecast);
	void UpdateWeatherForecast(int id, WeatherForecast forecast);
	void DeleteWeatherForecast(int id);
}

public class WeatherService : IWeatherService
{
	private readonly ConcurrentDictionary<int, WeatherForecast> _forecasts = new();
	private int _nextId = 1;

	public IEnumerable<WeatherForecast> GetWeatherForecasts() => _forecasts.Values;

	public WeatherForecast? GetWeatherForecast(int id)
	{
		_forecasts.TryGetValue(id, out var forecast);
		return forecast;
	}

	public void AddWeatherForecast(WeatherForecast forecast)
	{
		forecast.Id = _nextId++;
		_forecasts[forecast.Id] = forecast;
	}

	public void UpdateWeatherForecast(int id, WeatherForecast forecast)
	{
		if (_forecasts.ContainsKey(id))
		{
			forecast.Id = id;
			_forecasts[id] = forecast;
		}
	}

	public void DeleteWeatherForecast(int id)
	{
		_forecasts.TryRemove(id, out _);
	}
}
