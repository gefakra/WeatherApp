using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services;

/// <summary>
/// Сервис хранения последнего введённого города.
/// </summary>
public class StorageService : IStorageService
{
    private const string LastCityKey = "LastCity";

    public void SaveLastCity(string cityName) =>
        Preferences.Set(LastCityKey, cityName);

    public string GetLastCity() =>
        Preferences.Get(LastCityKey, string.Empty);
}
