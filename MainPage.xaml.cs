using WeatherApp.ViewModels;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)//ViewModels.MainViewModel viewModel)
    {
        InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
        BindingContext = viewModel;
    }

}


