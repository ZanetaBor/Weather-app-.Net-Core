using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Weather.MyServices
{
	public class WeatherService : IWeatherService
	{
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string ApiKey;
        public WeatherService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            ApiKey = _configuration["ApiSettings:ApiKey"];
        }

		public async Task<List<WeatherData>> GetWeatherDataAsync(List<string> cities)
		{
			{
				List<WeatherData> weatherDataList = new List<WeatherData>();

				foreach (var city in cities)
				{
					var weatherData = await GetWeatherAsync(city);
					weatherDataList.Add(weatherData);
				}

				return weatherDataList;
			}
			async Task<WeatherData> GetWeatherAsync(string city)
			{
				string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric";
				try
				{
					HttpResponseMessage response = await _httpClient.GetAsync(url);
					response.EnsureSuccessStatusCode();
					string responseBody = await response.Content.ReadAsStringAsync();

					var root = JsonConvert.DeserializeObject<Root>(responseBody);

					var weatherData = new WeatherData
					{
						City = root.City,
						Description = root.Weather != null && root.Weather.Count > 0 ? root.Weather[0].Description : "No description",
						Temperature = root.Main?.Temp ?? 0,
						Humidity = root.Main?.Humidity ?? 0,
						WindSpeed = root.Wind?.WindSpeed ?? 0
					};

					return weatherData;
				}
				catch (HttpRequestException e)
				{
					Console.WriteLine("Request error: " + e.Message);
				}
				catch (JsonException e)
				{
					Console.WriteLine("JSON error: " + e.Message);
				}

				return null;
			}
		}
	}
}