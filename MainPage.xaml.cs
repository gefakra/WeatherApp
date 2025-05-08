using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.ViewModels;


namespace WeatherApp;

public partial class MainPage : ContentPage
{

    // const string apiKey = "a1f8bf2c2c8a0c9d30533dcf4c3446f3"; // ⛅ Вставь сюда свой API-ключ OpenWeatherMap
    // const string apiUrl = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

    public MainPage(MainViewModel viewModel)//ViewModels.MainViewModel viewModel)
    {
        InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
        BindingContext = viewModel;
    }

}


