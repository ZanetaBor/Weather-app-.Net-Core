using Weather.Models;

namespace Weather.MyServices
{
	public interface IWeatherService
	{
		Task<List<WeatherData>> GetWeatherDataAsync(List<string> cities);

	}
}
