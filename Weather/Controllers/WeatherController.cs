using Weather.MyServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Weather.MyServices.IWeatherService;

namespace Weather.Controllers
{
	public class WeatherController : Controller
	{

		private readonly IWeatherService weatherService;

		public WeatherController(IWeatherService weatherService)
		{
			this.weatherService = weatherService;
		}

		public async Task<IActionResult> Index(string sortOrder, double? minTemperature)
		{
			try
			{
				ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "city";
				ViewBag.TemperatureSortParm = sortOrder == "Temperature" ? "temp_desc" : "Temperature";
				ViewBag.WindSpeedSortParm = sortOrder == "WindSpeed" ? "windSpeed_desc" : "WindSpeed";

				List<string> cities = new List<string> { "Gdynia", "New York", "London", "Paris", "Warszawa", "Rome", "Milano", "Barcelona" };

				var weatherDataList = await weatherService.GetWeatherDataAsync(cities);

				if (minTemperature.HasValue)
				{
					weatherDataList = weatherDataList.Where(w => w.Temperature >= minTemperature.Value).ToList();
				}

				switch (sortOrder)
				{
					case "city_desc":
						weatherDataList = weatherDataList.OrderByDescending(w => w.City).ToList();
						break;
					case "Temperature":
						weatherDataList = weatherDataList.OrderBy(w => w.Temperature).ToList();
						break;
					case "temp_desc":
						weatherDataList = weatherDataList.OrderByDescending(w => w.Temperature).ToList();
						break;
					case "WindSpeed":
						weatherDataList = weatherDataList.OrderBy(w => w.WindSpeed).ToList();
						break;
					case "windSpeed_desc":
						weatherDataList = weatherDataList.OrderByDescending(w => w.WindSpeed).ToList();
						break;
					default:
						weatherDataList = weatherDataList.OrderBy(w => w.City).ToList();
						break;
				}

				return View(weatherDataList);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

