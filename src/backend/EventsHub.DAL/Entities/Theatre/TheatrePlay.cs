using System;
using System.Collections.Generic;
using System.Text;

namespace EventsHub.DAL.Entities.Theatre
{
    public class TheatrePlay
    {
        public int TheatrePlayId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
    }
}
