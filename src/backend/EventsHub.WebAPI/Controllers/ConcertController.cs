using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private readonly IConcertService concertService;

        public ConcertController(IConcertService concertService)
        {
            this.concertService = concertService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcert(int id)
        {
            var concert = await concertService.GetConcert(id);
            if (concert == null) return NotFound();

            return Ok(concert);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConcerts([FromQuery] FilterParams filterParams)
        {
            var concerts = await concertService.GetAllConcerts(filterParams);
            if (!concerts.Any()) return NotFound();

            return Ok(concerts);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetConcertsCount()
        {
            var count = await concertService.GetConcertsCount();

            return Ok(count);
        }
    }
}
