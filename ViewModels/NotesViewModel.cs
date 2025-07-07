using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using Microsoft.Maui.Controls;

namespace WeatherApp.ViewModels;

public class NotesViewModel : INotifyPropertyChanged
{
    private readonly INoteService _noteService;
    private readonly IWeatherService _weatherService;
    public ICommand DeleteNoteCommand { get; }
    public ICommand SaveNoteCommand { get; }

    public ObservableCollection<Note> Notes { get; } = new();

    private DateTime _selectedDate = DateTime.Today;
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (_selectedDate != value)
            {
                _selectedDate = value;
                OnPropertyChanged();
                _ = LoadNotesAsync();
            }
        }
    }

    private Note _selectedNote;
    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            if (_selectedNote != value)
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }
    }

    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    private string _noteText;
    public string NoteText
    {
        get => _noteText;
        set
        {
            _noteText = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddNoteCommand { get; }

    public NotesViewModel(INoteService noteService, IWeatherService weatherService)
    {
        _noteService = noteService;
        _weatherService = weatherService;
        AddNoteCommand = new Command(async () => await AddNoteAsync());
        _ = LoadNotesAsync();
        DeleteNoteCommand = new Command(async () => await DeleteSelectedNoteAsync());
        SaveNoteCommand = new Command(async () => await SaveNoteAsync());
    }

    private async Task LoadNotesAsync()
    {
        Notes.Clear();
        var all = await _noteService.GetNotesAsync();
        foreach (var note in all.Where(n => n.CreatedAt.Date == SelectedDate.Date))
            Notes.Add(note);
    }

    private async Task AddNoteAsync()
    {
        string title = await Application.Current.MainPage.DisplayPromptAsync("Новая заметка", "Заголовок:", "OK", "Отмена");
        if (string.IsNullOrWhiteSpace(title)) return;

        string content = await Application.Current.MainPage.DisplayPromptAsync("Новая заметка", "Содержание:", "OK", "Отмена");
        if (content == null) return;

        var weather = await _weatherService.GetWeatherAsync(/*city*/ await GetCurrentCity());
        var note = new Note
        {
            Title = title,
            NoteText = content,
            CreatedAt = SelectedDate,
            IsActive = true,
            Location = /*city*/ await GetCurrentCity(),
            WeatherCondition = weather.Description
        };
        await _noteService.SaveNoteAsync(note);
        await LoadNotesAsync();
    }

    private async Task DeleteSelectedNoteAsync()
    {
        if (SelectedNote == null)
            return;

        bool confirmed = await Application.Current.MainPage.DisplayAlert(
            "Удаление заметки",
            $"Удалить заметку \"{SelectedNote.Title}\"?",
            "Удалить", "Отмена");

        if (!confirmed) return;

        await _noteService.DeleteNoteAsync(SelectedNote);
        await LoadNotesAsync();

        SelectedNote = null;
    }

    private async Task SaveNoteAsync()
    {
        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(NoteText))
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Заполните все поля", "OK");
            return;
        }

        var weather = await _weatherService.GetWeatherAsync(await GetCurrentCity());
        var note = new Note
        {
            Title = Title,
            NoteText = NoteText,
            CreatedAt = SelectedDate,
            IsActive = true,
            Location = await GetCurrentCity(),
            WeatherCondition = weather.Description
        };

        await _noteService.SaveNoteAsync(note);
        await LoadNotesAsync();

        // Очистка полей
        Title = string.Empty;
        NoteText = string.Empty;
    }

    private async Task<string> GetCurrentCity()
    {
        // если используешь cityService, то:
        // return _cityService.City;
        // иначе кул хардкод:
        return await Task.FromResult("Москва");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string p = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}
