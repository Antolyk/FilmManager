using FilmManager.Data.Models;
using FilmManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmManager.Data.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly FilmDbContext _context;

        public FilmRepository(FilmDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Film>> GetFilteredAndSortedFilmsAsync(
        string? genre = null,
        string? sortBy = null,
        bool ascending = true)
        {
            var query = _context.Films.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(f => f.Genre.ToLower() == genre.ToLower());
            }

            query = sortBy?.ToLower() switch
            {
                "rating" => ascending
                    ? query.OrderBy(f => f.Rating)
                    : query.OrderByDescending(f => f.Rating),
                "releaseyear" => ascending
                    ? query.OrderBy(f => f.ReleaseYear)
                    : query.OrderByDescending(f => f.ReleaseYear),
                _ => query
            };

            return await query.ToListAsync();
        }


        public async Task<Film?> GetByIdAsync(int id)
        {
            return await _context.Films.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task AddAsync(Film film)
        {
            await _context.Films.AddAsync(film);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var film = await GetByIdAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Film film)
        {
            _context.Films.Update(film);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
