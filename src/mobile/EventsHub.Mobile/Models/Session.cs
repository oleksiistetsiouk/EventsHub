using System;

namespace EventsHub.Mobile.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string DirectLink { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public bool IsShown { get; set; }
        public string SessionType { get; set; }
    }
}