using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService filmService;

        public FilmController(IFilmService filmService)
        {
            this.filmService = filmService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            var film = await filmService.GetFilm(id);
            if (film == null) return NotFound();

            return Ok(film);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTheatrePlays([FromQuery] FilterParams filterParams)
        {
            var films = await filmService.GetAllFilms(filterParams);
            if (!films.Any()) return NotFound();

            return Ok(films);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetFilmsCount()
        {
            var count = await filmService.GetFilmsCount();

            return Ok(count);
        }
    }
}
