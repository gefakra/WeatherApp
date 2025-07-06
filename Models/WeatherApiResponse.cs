namespace WeatherApp.Models
{
    public class WeatherApiResponse
    {
        public List<WeatherDescription> Weather { get; set; }
        public MainInfo Main { get; set; }
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class MainInfo
    {
        public float Temp { get; set; }
    }
}
