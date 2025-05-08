namespace WeatherApp.Services.Interfaces
{
    public interface IConfigurationService
    {
        string GetApiKey();
        string GetApiBaseUrl();
    }
}