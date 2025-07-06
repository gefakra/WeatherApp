//using Android.Net;
using System.Net.Http.Json;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services;

/// <summary>
/// Сервис получения данных о погоде через OpenWeatherMap API.
/// </summary>
public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    private readonly IConfigurationService _configService;
    public WeatherService(IConfigurationService configService, HttpClient httpClient)
    {
        _configService = configService;
        _httpClient = httpClient;
    }

    public async Task<WeatherInfo> GetWeatherAsync(string city)
    {        
        var url = $"{_configService.GetApiBaseUrl()}weather?q={city}&appid={_configService.GetApiKey()}&units=metric&lang=ru";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Ошибка при запросе погоды: {response.StatusCode}");

        var data = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();
        if (data == null)
            throw new Exception("Ошибка разбора ответа API.");

        return new WeatherInfo
        {
            Temperature = (int)data.Main.Temp,
            Description = data.Weather[0].Description,
            Icon = GetIconName(data.Weather[0].Main)
        };
    }

    public Task<List<WeatherInfo>> GetForecastAsync(string city)
    {
        // Возвращаем пустой список пока не реализовано
        return Task.FromResult(new List<WeatherInfo>());
    }

    private static string GetIconName(string condition) => condition switch
    {
        "Clouds" => "cloudy.png",
        "Rain" => "rain.png",
        "Clear" => "sunny.png",
        _ => "standard.png"
    };   
}
