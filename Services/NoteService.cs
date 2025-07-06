using SQLite;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class NoteService : INoteService
    {
            private readonly SQLiteAsyncConnection _database;

            public NoteService()
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");
                _database = new SQLiteAsyncConnection(dbPath);
                _database.CreateTableAsync<Note>().Wait();
            }

            public Task<List<Note>> GetNotesAsync()
            {
                return _database.Table<Note>().ToListAsync();
            }

            public Task<Note> GetNoteAsync(int id)
            {
                return _database.Table<Note>().Where(i => i.Id == id).FirstOrDefaultAsync();
            }

            public Task<int> SaveNoteAsync(Note note)
            {
                if (note.Id != 0)
                    return _database.UpdateAsync(note);
                else
                    return _database.InsertAsync(note);
            }

            public Task<int> DeleteNoteAsync(Note note)
            {
                return _database.DeleteAsync(note);
            }
        
    }
}