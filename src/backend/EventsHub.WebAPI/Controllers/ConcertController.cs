using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsHub.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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

            return Ok(concert);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConcerts([FromQuery] FilterParams filterParams)
        {
            var concerts = await concertService.GetAllConcerts(filterParams);

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
