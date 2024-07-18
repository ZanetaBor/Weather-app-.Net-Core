using Newtonsoft.Json;

// Models/WeatherData.cs
namespace Weather.Models
{
	public class WeatherData
	{
		public string City { get; set; }
		public string Description { get; set; }
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public double WindSpeed { get; set; }
	}
}

// Models/Main.cs
namespace Weather.Models
{
	public class Main
	{
		[JsonProperty("temp")]
		public double Temp { get; set; }

		[JsonProperty("humidity")]
		public double Humidity { get; set; }
	}
}

// Models/Wind.cs

namespace Weather.Models
{
	public class Wind
	{
		[JsonProperty("speed")]
		public double WindSpeed { get; set; }

	}
}

// Models/WeatherDescription.cs
namespace Weather.Models
{
	public class WeatherDescription
	{
		[JsonProperty("description")]
		public string Description { get; set; }
	}
}

// Models/Root.cs
namespace Weather.Models
{
	public class Root
	{
		[JsonProperty("name")]
		public string City { get; set; }

		[JsonProperty("weather")]
		public List<WeatherDescription> Weather { get; set; }

		[JsonProperty("main")]
		public Main Main { get; set; }

		[JsonProperty("wind")]
		public Wind Wind { get; set; }
	}
}
