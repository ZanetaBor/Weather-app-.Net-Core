using Microsoft.Extensions.Configuration;
using Weather.MyServices;

namespace Weather
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddHttpClient();
			builder.Services.AddScoped<IWeatherService, WeatherService>();
            
			// Add UserSecrets
			builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			var app = builder.Build();

            string ApiKey = builder.Configuration["ApiSettings:ApiKey"];
           
			// Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Weather}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
