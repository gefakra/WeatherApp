using WeatherApp.Services.Interfaces;
using Microsoft.Maui.Storage;

namespace WeatherApp.Services
{
    public class StorageService : IStorageService
    {
        private const string LastCityKey = "LastCity";

        public void SaveLastCity(string cityName)
        {
            Preferences.Set(LastCityKey, cityName);
        }

        public string GetLastCity()
        {
            return Preferences.Get(LastCityKey, string.Empty);
        }
    }
}
