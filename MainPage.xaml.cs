namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private WeatherService _weatherService;

        public MainPage()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
        }

        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {
            string city = CityEntry.Text;  // Вводим город
            var weatherData = await _weatherService.GetWeatherAsync(city);

            if (weatherData != null)
            {
                WeatherLabel.Text = $"{weatherData.Name}: {weatherData.Main.Temp}°C\n" +
                                    $"Humidity: {weatherData.Main.Humidity}%";
            }
            else
            {
                WeatherLabel.Text = "Error fetching weather data.";
            }
        }
    }

}
