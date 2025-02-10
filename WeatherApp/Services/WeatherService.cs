using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private const string ApiKey = "44c43431c6685c7b10a42c1a512683ab";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        public async Task<WeatherModel> GetWeatherAsync(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{BaseUrl}?q={city}&appid={ApiKey}&units=metric&lang=en");

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
