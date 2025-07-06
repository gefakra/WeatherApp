using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AppConfiguration _config;

        public ConfigurationService(AppConfiguration config)
        {
            _config = config;
        }

        public string GetApiKey() => _config.ApiKey;
        public string GetApiBaseUrl() => _config.ApiBaseUrl;
        public string GetCity() => _config.City;
    }
}