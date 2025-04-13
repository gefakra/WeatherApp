using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService
{
    private const string ApiKey = "YOUR_API_KEY"; // Замените на свой API-ключ
    private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        using (var client = new HttpClient())
        {
            var url = string.Format(ApiUrl, city, ApiKey);
            var response = await client.GetStringAsync(url);
            var weatherData = JsonConvert.DeserializeObject<WeatherData>(response);
            return weatherData;
        }
    }
}

public class WeatherData
{
    public MainData Main { get; set; }
    public string Name { get; set; }
}

public class MainData
{
    public float Temp { get; set; }
    public float Humidity { get; set; }
}