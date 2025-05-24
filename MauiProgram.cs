using System.Reflection;
using System.Text.Json;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }); 
        


        // Загрузка AppConfig.json
        var configPath = Path.Combine(FileSystem.AppDataDirectory, "AppConfig.json");

        if (!File.Exists(configPath))
        {
            using var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("WeatherApp.Resources.Configuration.AppConfig.json");

            if (stream is null)
                throw new FileNotFoundException("Embedded resource AppConfig.json не найдена.");

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            File.WriteAllText(configPath, json);
        }

        var jsonText = File.ReadAllText(configPath);
        var config = JsonSerializer.Deserialize<AppConfiguration>(jsonText);

        // DI
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton(config);
        builder.Services.AddSingleton<IWeatherService, WeatherService>();        
        builder.Services.AddSingleton<IStorageService, StorageService>();
        builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<MainPage>();


        return builder.Build();
    }
}
