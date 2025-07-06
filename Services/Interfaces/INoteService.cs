using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface INoteService
    {
        Task<List<Note>> GetNotesAsync();
        Task<Note> GetNoteAsync(int id);
        Task<int> SaveNoteAsync(Note note);
        Task<int> DeleteNoteAsync(Note note);
    }
}