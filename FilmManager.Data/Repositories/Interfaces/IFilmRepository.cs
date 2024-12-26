using FilmManager.Data.Models;

namespace FilmManager.Data.Repositories.Interfaces
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> GetFilteredAndSortedFilmsAsync(string? genre = null, string? sortBy = null, bool ascending = true);
        Task<Film?> GetByIdAsync(int id);
        Task AddAsync(Film film);
        Task UpdateAsync(Film film); //id?
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
