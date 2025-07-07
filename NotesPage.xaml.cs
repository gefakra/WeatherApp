using WeatherApp.ViewModels;

namespace WeatherApp;
[QueryProperty(nameof(City), "city")]
public partial class NotesPage : ContentPage
{
    
    public string City { get; set; }    
    

    public NotesPage(NotesViewModel viewMode)
    {
        InitializeComponent();
        BindingContext = viewMode;
    }
}