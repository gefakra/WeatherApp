using WeatherApp.ViewModels;

namespace WeatherApp;

public partial class CalendarPage : ContentPage
{
	public CalendarPage(NotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}