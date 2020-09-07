using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsHub.WebAPI.Controllers
{
    [Authorize]
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

            return Ok(film);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilms([FromQuery] FilterParams filterParams)
        {
            var films = await filmService.GetAllFilms(filterParams);

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
