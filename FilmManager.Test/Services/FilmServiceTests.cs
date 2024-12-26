using FilmManager.Data;
using FilmManager.Data.Models;
using FilmManager.Data.Repositories;
using FilmManager.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.Test.Services
{
    public class FilmServiceIntegrationTests
    {
        private readonly FilmService _filmService;
        private readonly FilmRepository _filmRepository;
        private readonly FilmDbContext _dbContext;

        public FilmServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<FilmDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new FilmDbContext(options);
            _filmRepository = new FilmRepository(_dbContext);
            _filmService = new FilmService(_filmRepository);
        }

        [Fact]
        public async Task AddFilmAsync_ShouldAddFilmToDatabase()
        {
            // Arrange
            var filmDto = new FilmDto
            {
                Title = "Test Title",
                Genre = "Test Genre",
                Director = "Test Director",
                ReleaseYear = 2010,
                Rating = 9,
                Description = "Test Description"
            };

            // Act
            var result = await _filmService.AddFilmAsync(filmDto);

            // Assert
            var filmInDb = await _dbContext.Films.FindAsync(result.Id);
            Assert.NotNull(filmInDb);
            Assert.Equal(filmInDb.Title, filmDto.Title);
            Assert.Equal(filmInDb.Director, filmDto.Director);
            Assert.Equal(filmInDb.Description, filmDto.Description);

            //Dispose
            Dispose();
        }


        [Fact]
        public async Task GetFilmById_ShouldReturnFilm()
        {
            // Arrange
            var filmDto = new FilmDto
            {
                Title = "Test Title",
                Genre = "Test Genre",
                Director = "Test Director",
                ReleaseYear = 2000,
                Rating = 9,
                Description = "Test Description"
            };

            await _filmService.AddFilmAsync(filmDto);
            var filmId = _dbContext.Films.FirstOrDefault(f => f.Title == filmDto.Title).Id;

            // Act
            var result = await _filmService.GetFilmByIdAsync(filmId);

            //Assert
            var filmInDb = await _dbContext.Films.FirstOrDefaultAsync(f => f.Id == result.Id);
            Assert.NotNull(filmInDb);
            Assert.Equal(filmInDb.Title, filmDto.Title);
            Assert.Equal(filmInDb.Director, filmDto.Director);
            Assert.Equal(filmInDb.Description, filmDto.Description);

            //Dispose
            Dispose();
        }

        [Fact]
        public async Task UpdateFilm_ShouldUpdateFilm()
        {
            //Arrange
            var filmDtoCreate = new FilmDto
            {
                Title = "Test Title",
                Genre = "Test Genre",
                Director = "Test Director",
                ReleaseYear = 2001,
                Rating = 8,
                Description = "Test Description"
            };

            var filmDtoUpdate = new FilmDto
            {
                Title = "Test Title 2",
                Genre = "Test Genre 2",
                Director = "Test Director 2",
                ReleaseYear = 2000,
                Rating = 9,
                Description = "Test Description 2"
            };

            await _filmService.AddFilmAsync(filmDtoCreate);
            var filmId = _dbContext.Films.FirstOrDefault(f => f.Title == filmDtoCreate.Title).Id;

            //Act
            var result = await _filmService.UpdateFilmAsync(filmId, filmDtoUpdate);

            //Assert
            var filmInDb = await _dbContext.Films.FirstOrDefaultAsync(f => f.Id == result.Id);
            Assert.NotNull(filmInDb);
            Assert.Equal(filmInDb.Title, filmDtoUpdate.Title);
            Assert.Equal(filmInDb.Director, filmDtoUpdate.Director);
            Assert.Equal(filmInDb.Description, filmDtoUpdate.Description);

            //Dispose
            Dispose();
        }

        [Fact]
        public async Task DeleteFilm_ShouldDeleteFilm()
        {
            var filmDto = new FilmDto
            {
                Title = "Test Title",
                Genre = "Test Genre",
                Director = "Test Director",
                ReleaseYear = 2000,
                Rating = 9,
                Description = "Test Description"
            };

            await _filmService.AddFilmAsync(filmDto);
            var filmId = _dbContext.Films.FirstOrDefault(f => f.Title == filmDto.Title).Id;

            // Act
            var result = await _filmService.DeleteFilmAsync(filmId);

            //Assert
            var filmInDb = await _dbContext.Films.FirstOrDefaultAsync(f => f.Id == filmId);
            Assert.Null(filmInDb);
            Assert.Equal(result, "Film was succesfully deleted!");

            //Dispose
            Dispose();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}