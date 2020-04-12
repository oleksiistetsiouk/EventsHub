using System;

namespace EventsHub.BLL.DTO
{
    public class TheatrePlayDto
    {
        public int TheatrePlayId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
    }
}
