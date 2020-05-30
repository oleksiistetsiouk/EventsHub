using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TheatreController : ControllerBase
    {
        private readonly ITheatreService theatreService;

        public TheatreController(ITheatreService theatreService)
        {
            this.theatreService = theatreService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTheatrePlay(int id)
        {
            var play = await theatreService.GetTheatrePlay(id);
            if (play == null) return NotFound();

            return Ok(play);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTheatrePlays([FromQuery] FilterParams filterParams)
        {
            var plays = await theatreService.GetAllTheatrePlays(filterParams);
            if (!plays.Any()) return NotFound();

            return Ok(plays);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetPlaysCount()
        {
            var count = await theatreService.GetPlaysCount();

            return Ok(count);
        }
    }
}
