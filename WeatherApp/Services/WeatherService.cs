using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly string _apiKey;

        public WeatherService(IConfiguration configuration)
        {
            _apiKey = configuration["AppSettings:ApiKey"];
        }
        
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        public async Task<WeatherModel> GetWeatherAsync(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{BaseUrl}?q={city}&appid={_apiKey}&units=metric&lang=en");

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"API request failed: {response.StatusCode} - {response.ReasonPhrase}");
                        return null;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(json);

                    return JsonConvert.DeserializeObject<WeatherModel>(json);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP request error: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
