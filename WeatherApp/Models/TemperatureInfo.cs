using Newtonsoft.Json;

namespace WeatherApp.Models
{
    public class TemperatureInfo
    {
        [JsonProperty("temp")]
        public float Temp { get; set; }

        [JsonProperty("temp_min")]
        public float Temp_Min { get; set; }

        [JsonProperty("temp_max")]
        public float Temp_Max { get; set; }
    }
}
