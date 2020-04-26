using System.Collections.Generic;

namespace EventsHub.Mobile.Models
{
    public class Cinema
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string Address { get; set; }
        public List<Session> Sessions { get; set; }
    }
}