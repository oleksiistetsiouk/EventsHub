using EventsHub.BLL.DTO;
using EventsHub.Common.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsHub.BLL.Interfaces
{
    public interface IConcertService
    {
        Task<ConcertDto> GetConcert(int id);
        Task<IEnumerable<ConcertDto>> GetAllConcerts(FilterParams filterParams);
    }
}
