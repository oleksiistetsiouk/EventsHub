using System.Collections.Generic;

namespace EventsHub.BLL.DTO
{
    public class CinemaDto
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string Address { get; set; }
        public List<SessionDto> Sessions { get; set; }
    }
}
