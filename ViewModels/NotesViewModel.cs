using SQLitePCL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly INoteService _noteService;
        private readonly IWeatherService _weatherService;
        private readonly IConfigurationService _configService;

        public ObservableCollection<Note> Notes { get; set; } = new();

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetProperty(ref _selectedNote, value);
                if (_selectedNote != null)
                {
                    Title = _selectedNote.Title;
                    NoteText = _selectedNote.NoteText;
                }
            }
        }
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public string Title { get; set; }
        public string NoteText { get; set; }

        public ICommand LoadNotesCommand { get; }
        public ICommand SaveNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        public NotesViewModel(INoteService noteService,IWeatherService weatherService, IConfigurationService configService)
        {
            _noteService = noteService;
            _weatherService = weatherService;
            _configService = configService;

            LoadNotesCommand = new Command(async () => await LoadNotesAsync());
            SaveNoteCommand = new Command(async () => await SaveNoteAsync());
            DeleteNoteCommand = new Command(async () => await DeleteNoteAsync());

            _ = LoadNotesAsync();
        }

        private async Task LoadNotesAsync()
        {
            Notes.Clear();
            var notes = await _noteService.GetNotesAsync();
            foreach (var note in notes.OrderByDescending(n => n.CreatedAt))
                Notes.Add(note);
        }

        private async Task SaveNoteAsync()
        {
            if (!string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(NoteText))
            {
                var city = _configService.GetCity();
                var weather = await _weatherService.GetWeatherAsync(city);

                var note = new Note
                {
                    Title = Title,
                    NoteText = NoteText,
                    CreatedAt = SelectedDate,
                    IsActive = true,
                    Location = city,
                    WeatherCondition = weather.Description
                };

                await _noteService.SaveNoteAsync(note);
                await LoadNotesAsync();
                Title = NoteText = string.Empty;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(NoteText));
            }
        }

        private async Task DeleteNoteAsync()
        {
            if (SelectedNote != null)
            {
                await _noteService.DeleteNoteAsync(SelectedNote);
                await LoadNotesAsync();
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
