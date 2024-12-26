using FilmManager.Data.Models;
using FilmManager.Data.Repositories.Interfaces;
using FilmManager.Service.Interfaces;
using FilmManager.Service.Models;

namespace FilmManager.Service
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<IEnumerable<FilmServiceModel>> GetFilteredAndSortedFilmsAsync(string? genre, string? sortBy, bool ascending)
        {
            var films = await _filmRepository.GetFilteredAndSortedFilmsAsync(genre, sortBy, ascending);

            return films.Select(film => new FilmServiceModel
            {
                Id = film.Id,
                Title = film.Title,
                Genre = film.Genre,
                Director = film.Director,
                ReleaseYear = film.ReleaseYear,
                Rating = film.Rating,
                Description = film.Description
            });
        }


        public async Task<FilmServiceModel> GetFilmByIdAsync(int id)
        {
            var film = await _filmRepository.GetByIdAsync(id);

            if (film == null)
                return null;

            FilmServiceModel filmModel = new FilmServiceModel
            {
                Id = film.Id,
                Title = film.Title,
                Genre = film.Genre,
                Director = film.Director,
                ReleaseYear = film.ReleaseYear,
                Rating = film.Rating,
                Description = film.Description
            };

            return filmModel;
        }
        public async Task<FilmServiceModel> AddFilmAsync(FilmDto request)
        {
            Film film = new Film
            {
                Title = request.Title,
                Genre = request.Genre,
                Director = request.Director,
                ReleaseYear = request.ReleaseYear.Value,
                Rating = request.Rating.Value,
                Description = request.Description
            };

            await _filmRepository.AddAsync(film);
            await _filmRepository.SaveChangesAsync();

            FilmServiceModel filmModel = new FilmServiceModel
            {
                Id = film.Id,
                Title = film.Title,
                Genre = film.Genre,
                Director = film.Director,
                ReleaseYear = film.ReleaseYear,
                Rating = film.Rating,
                Description = film.Description
            };

            return filmModel;
        }
        public async Task<FilmServiceModel> UpdateFilmAsync(int id, FilmDto request)
        {
            var film = await _filmRepository.GetByIdAsync(id);
            if (film != null)
            {
                if (!string.IsNullOrEmpty(request.Title)) film.Title = request.Title;
                if (!string.IsNullOrEmpty(request.Genre)) film.Genre = request.Genre;
                if (!string.IsNullOrEmpty(request.Director)) film.Director = request.Director;
                if (request.ReleaseYear.HasValue) film.ReleaseYear = request.ReleaseYear.Value;
                if (request.Rating.HasValue) film.Rating = request.Rating.Value;
                if (!string.IsNullOrEmpty(request.Description)) film.Description = request.Description;

                await _filmRepository.UpdateAsync(film);
                await _filmRepository.SaveChangesAsync();

                FilmServiceModel filmModel = new FilmServiceModel
                {
                    Id = film.Id,
                    Title = film.Title,
                    Genre = film.Genre,
                    Director = film.Director,
                    ReleaseYear = film.ReleaseYear,
                    Rating = film.Rating,
                    Description = film.Description
                };

                return filmModel;
            }
            return null;
        }
        public async Task<string> DeleteFilmAsync(int id)
        {
            var film = await _filmRepository.GetByIdAsync(id);
            if (film != null)
            {
                await _filmRepository.DeleteAsync(id);
                await _filmRepository.SaveChangesAsync();

                return "Film was succesfully deleted!";
            }
            return "Film was not deleted!";
        }
    }
}
