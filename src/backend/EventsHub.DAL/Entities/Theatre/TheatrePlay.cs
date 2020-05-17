using System;

namespace EventsHub.DAL.Entities.Theatre
{
    public class TheatrePlay
    {
        public int TheatrePlayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public DateTime Date { get; set; }
        public string PosterUrl { get; set; }
        public string DirectLink { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
