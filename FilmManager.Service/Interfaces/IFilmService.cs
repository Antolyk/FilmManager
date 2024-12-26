using FilmManager.Data.Models;
using FilmManager.Service.Models;

namespace FilmManager.Service.Interfaces
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmServiceModel>> GetFilteredAndSortedFilmsAsync(string? genre, string? sortBy, bool ascending);
        Task<FilmServiceModel> GetFilmByIdAsync(int id);
        Task<FilmServiceModel> AddFilmAsync(FilmDto request);
        Task<FilmServiceModel> UpdateFilmAsync(int id, FilmDto request);
        Task<string> DeleteFilmAsync(int id);
    }
}
