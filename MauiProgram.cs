﻿using Microsoft.Extensions.Logging;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;
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

       
        builder.Services.AddSingleton<IWeatherService, WeatherService>();

        builder.Services.AddSingleton<IStorageService, StorageService>();
        

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<MainPage>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
