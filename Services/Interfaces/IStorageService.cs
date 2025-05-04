namespace WeatherApp.Services.Interfaces
{
    public interface IStorageService
    {
        void SaveLastCity(string cityName);
        string GetLastCity();
    }
}
