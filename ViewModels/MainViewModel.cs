using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IWeatherService _weatherService;
    private readonly IStorageService _storageService;

    private string _cityName;
    public string CityName
    {
        get => _cityName;
        set => SetProperty(ref _cityName, value);
    }

    private WeatherInfo _weatherInfo;
    public WeatherInfo WeatherInfo
    {
        get => _weatherInfo;
        set => SetProperty(ref _weatherInfo, value);
    }

    public ICommand GetWeatherCommand { get; }

    public MainViewModel(IWeatherService weatherService, IStorageService storageService)
    {
        _weatherService = weatherService;
        _storageService = storageService;
       

        GetWeatherCommand = new Command(async () => await GetWeatherAsync());

        CityName = _storageService.GetLastCity();
        if (!string.IsNullOrWhiteSpace(CityName))
            GetWeatherCommand.Execute(null);
    }

    private async Task GetWeatherAsync()
    {
        if (string.IsNullOrWhiteSpace(CityName)) return;

        WeatherInfo = await _weatherService.GetWeatherAsync(CityName);
        var forecast = await _weatherService.GetForecastAsync(CityName);
        _storageService.SaveLastCity(CityName);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}
