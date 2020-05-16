using EventsHub.BLL.DTO;
using EventsHub.Common.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsHub.BLL.Interfaces
{
    public interface ITheatreService
    {
        Task<TheatrePlayDto> GetTheatrePlay(int id);
        Task<IEnumerable<TheatrePlayDto>> GetAllTheatrePlays(FilterParams filterParams);
        Task<int> GetPlaysCount();
    }
}
