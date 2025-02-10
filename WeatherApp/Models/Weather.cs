using Newtonsoft.Json;

namespace WeatherApp.Models
{
    public class Weather
    {
        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
