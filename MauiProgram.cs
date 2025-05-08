//using Android.Net;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;
using WeatherApp.ViewModels;
using Plugin.Firebase;
using Plugin.Firebase.CloudMessaging;
namespace WeatherApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });
#if ANDROID
        CrossFirebase.Initialize(options => {
            options.AddCloudMessaging();
        });
#else
CrossFirebase.Initialize();
#endif


        var configPath = Path.Combine(FileSystem.AppDataDirectory, "AppConfig.json");

        if (!File.Exists(configPath))
        {
            using var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("WeatherApp.Resources.Configuration.AppConfig.json");

            if (stream == null)
                throw new FileNotFoundException("Embedded resource AppConfig.json не найдена.");

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            File.WriteAllText(configPath, json);
        }
        var jsonText = File.ReadAllText(configPath);
        var config = JsonSerializer.Deserialize<AppConfiguration>(jsonText);


        // Регистрация сервисов
        builder.Services.AddSingleton<IWeatherService, WeatherService>();
        builder.Services.AddHttpClient<IWeatherService, WeatherService>();
        builder.Services.AddSingleton<IStorageService, StorageService>();
        builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();

        // ViewModels и страницы
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton(config);
       

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();

    }
}
