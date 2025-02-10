using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService = new WeatherService();

        public IActionResult Index()
        {
            string lastCity = HttpContext.Session.GetString("LastCity") ?? "Kyiv";
            return View(new WeatherViewModel { City = lastCity });
        }


        [HttpPost]
        public async Task<ActionResult> GetWeather(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                TempData["Error"] = "Enter the city name!";
                return RedirectToAction("Index");
            }

            var weather = await _weatherService.GetWeatherAsync(city);
            if (weather == null || weather.Weather == null || weather.Weather.Length == 0 || weather.TemperatureInfo == null)
            {
                TempData["Error"] = "City not found or API error.";
                return RedirectToAction("Index");
            }


            HttpContext.Session.SetString("LastCity", city);

            bool isRain = weather.Weather != null
                && weather.Weather.Length > 0
                && weather.Weather[0].Main != null
                && weather.Weather[0].Main.ToLower().Contains("rain");

            if (isRain && string.IsNullOrEmpty(HttpContext.Session.GetString($"Warned_{city}")))
            {
                TempData["RainWarning"] = $"Attention! Rain is expected in {city}!";
                HttpContext.Session.SetString($"Warned_{city}", "true");
            }

            return View("Index", new WeatherViewModel
            {
                City = city,
                Temperature = weather.TemperatureInfo.Temp,
                TempMin = weather.TemperatureInfo.Temp_Min,
                TempMax = weather.TemperatureInfo.Temp_Max,
                Description = weather.Weather[0].Description
            });
        }
    }
}
