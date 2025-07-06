using WeatherApp.ViewModels;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
        BindingContext = viewModel;
    }
    private async void OnNextPageClicked(object sender, EventArgs e)
    {
        string city = CityName.Text?.Trim();

        if (!string.IsNullOrWhiteSpace(city))
        {
            await Shell.Current.GoToAsync($"notespage?city={city}");
        }
    }
}


