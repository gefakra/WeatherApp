using System.Buffers.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string _apiKey = "a1f8bf2c2c8a0c9d30533dcf4c3446f3";
        private const string _baseUrl = "https://api.openweathermap.org/data/2.5/";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherInfo> GetWeatherAsync(string city)
        {
            var url = $"{_baseUrl}/weather?q={city}&appid={_apiKey}&units=metric&lang=ru";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ошибка при запросе погоды: {response.StatusCode}");
            }

            var data = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();

            if (data == null)
                throw new Exception("Не удалось получить данные о погоде.");

            return new WeatherInfo
            {
                Temperature = (int)data.Main.Temp,
                Description = data.Weather[0].Description,
                Icon = GetIconName(data.Weather[0].Main)
            };
        }


        private string GetIconName(string main)
        {
            return main switch
            {
                "Clouds" => "cloudy.png",
                "Rain" => "rain.png",
                "Clear" => "sunny.png",
                _ => "standart.png"
            };
        }

        public Task<List<WeatherInfo>> GetForecastAsync(string city)
        {
            throw new NotImplementedException();
        }
    }
}
