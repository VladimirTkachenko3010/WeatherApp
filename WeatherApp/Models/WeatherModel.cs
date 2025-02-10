using Newtonsoft.Json;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        [JsonProperty("main")]
        public TemperatureInfo TemperatureInfo { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
