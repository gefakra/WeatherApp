using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.ViewModels
{
    /// <summary>
    /// ViewModel для главного экрана. Управляет погодной информацией и вводом города.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IWeatherService _weatherService;
        private readonly IStorageService _storageService;

        private string _cityName = string.Empty;
        private WeatherInfo? _weatherInfo;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel(IWeatherService weatherService, IStorageService storageService)
        {
            _weatherService = weatherService;
            _storageService = storageService;

            GetWeatherCommand = new Command(async () => await GetWeatherAsync());

            // Автоввод сохранённого города при запуске
            CityName = _storageService.GetLastCity();
            if (!string.IsNullOrWhiteSpace(CityName))
                _ = GetWeatherAsync();
        }

        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        public WeatherInfo? WeatherInfo
        {
            get => _weatherInfo;
            set => SetProperty(ref _weatherInfo, value);
        }

        public ICommand GetWeatherCommand { get; }

        public bool IsWeatherInfoVisible => WeatherInfo != null;

        /// <summary>
        /// Асинхронно запрашивает данные о погоде и сохраняет город.
        /// </summary>
        private async Task GetWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(CityName))
                return;

            try
            {
                WeatherInfo = await _weatherService.GetWeatherAsync(CityName);
                _storageService.SaveLastCity(CityName);
            }
            catch (Exception ex)
            {
                // TODO: Показать пользователю, если надо
                Console.WriteLine($"Ошибка при получении погоды: {ex.Message}");
            }
        }

        /// <summary>
        /// Упрощённая реализация INotifyPropertyChanged.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
