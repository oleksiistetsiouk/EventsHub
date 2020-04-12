using EventsHub.BLL.DTO;
using EventsHub.Common.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsHub.BLL.Interfaces
{
    public interface IFilmService
    {
        Task<FilmDto> GetFilm(int id);
        Task<IEnumerable<FilmDto>> GetAllFilms(FilterParams filterParams);
    }
}
