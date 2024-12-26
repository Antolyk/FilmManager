using FilmManager.Data.Models;
using FilmManager.Service.Interfaces;
using FilmManager.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;
        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        /// <summary>
        /// Отримує всі фільми з можливістю фільтрації та сортування.
        /// </summary>
        /// <remarks>
        /// Надсилає користувачу всі фільми відносно вказаних ним параметрів для фільтрації та/або сортування.
        /// У випадку якщо не буде вказано додаткових параметрів - повертаються всі фільми.
        /// </remarks>
        /// <param name="genre">Фільтрувати за жанром (опціонально).</param>
        /// <param name="sortBy">Сортувати за параметром (наприклад: Release Year, Rating) (опціонально).</param>
        /// <param name="ascending">Сортувати за зростанням (true) чи спаданням (false).</param>
        /// <returns>Список фільмів.</returns>
        [HttpGet("films")]
        public async Task<IActionResult> GetFilteredAndSortedFilms(
        [FromQuery] string? genre,
        [FromQuery] string? sortBy,
        [FromQuery] bool ascending = true)
        {
            var films = await _filmService.GetFilteredAndSortedFilmsAsync(genre, sortBy, ascending);
            if (films == null)
                return NotFound();
            return Ok(films);
        }

        /// <summary>
        /// Отримує фільм по Id.
        /// </summary>
        /// <remarks>
        /// Повертає користувачу фільм з тим самим Id, який він вказав в параметрах.
        /// </remarks>
        /// <param name="id">ID фільму який необхідно знайти.</param>
        /// <returns>Фільм.</returns>
        [HttpGet("films/{id}")]
        public async Task<ActionResult<FilmServiceModel>> GetFilmById(int id)
        {
            var film = await _filmService.GetFilmByIdAsync(id);
            if (film == null)
                return NotFound();

            return Ok(film);
        }

        /// <summary>
        /// Створює новий фільм з інформацією про нього від користувача.
        /// </summary>
        /// <remarks>
        /// Створює новий фільм з усією інформацією яку надав користувач та повертає його йому.
        /// Всі поля повинні бути заповнені.
        /// Обмеження по Release Year: 1900 - 2999
        /// Обмеження по Rating: 1.0 - 10.0
        /// </remarks>
        /// <param name="request">Інформація про фільм.</param>
        /// <returns>Фільм.</returns>
        [HttpPost("films")]
        public async Task<ActionResult<FilmServiceModel>> AddFilm([FromBody] FilmDto request)
        {
            var film = await _filmService.AddFilmAsync(request);
            return Ok(film);
        }

        /// <summary>
        /// Змінює інформацію знайденого по Id фільму на нову, вказану користувачем.
        /// </summary>
        /// <remarks>
        /// Знаходить фільм по Id який вказав користувач та замінює його дані на нові.
        /// Якщо певні дані не потребують заміни їх все одно потрібно продублювати, оскільки всі дані є необхідними.
        /// </remarks>
        /// <param name="id">ID фільму інформацію якого необхідно оновити.</param>
        /// <param name="request">Нова інформація про фільм.</param>
        /// <returns>Оновлений фільм.</returns>
        [HttpPut("films/{id}")] //Update Film by ID with data sent by user
        public async Task<ActionResult<FilmServiceModel>> UpdateFilm(int id, [FromBody] FilmDto request)
        {
            var film = await _filmService.UpdateFilmAsync(id, request);
            return Ok(film);
        }

        /// <summary>
        /// Видаляє фільм по Id.
        /// </summary>
        /// <remarks>
        /// Знаходить фільм по Id вказаному користувачем, видаляє його та повертає строку з повідомлення про вдачу або невдачу під час операції.
        /// </remarks>
        /// <param name="id">ID фільму який необхідно видалити.</param>
        /// <returns>Повідомлення про успіх/невдачу операції.</returns>
        [HttpDelete("films/{id}")] //Delete Film by ID and return messege
        public async Task<ActionResult<string>> DeleteFilm(int id)
        {
            return await _filmService.DeleteFilmAsync(id);
        }
    }
}
