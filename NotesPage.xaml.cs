using WeatherApp.ViewModels;

namespace WeatherApp;
[QueryProperty(nameof(City), "city")]
public partial class NotesPage : ContentPage
{
    
    public string City
    {
        set
        {
            if (BindingContext is NotesViewModel vm)
                vm.City = value;
        }
    }

    public NotesPage()
    {
        InitializeComponent();
        BindingContext = App.Current.Services.GetService<NotesViewModel>();
    }
}