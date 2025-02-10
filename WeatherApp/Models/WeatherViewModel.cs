namespace WeatherApp.Models
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public float Temperature { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public string Description { get; set; }
    }
}
