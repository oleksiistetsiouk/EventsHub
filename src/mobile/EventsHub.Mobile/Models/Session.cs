using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EventsHub.Mobile.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string DirectLink { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public bool IsShown => DateTime.Now > Time;
        public string SessionType { get; set; }
        public string PriceRange => PriceTo == 0 ? PriceFrom.ToString() : $"{PriceFrom}-{PriceTo}";
        public ICommand OpenWebCommand { get; }

        public Session()
        {
            OpenWebCommand = new Command<string>(async (string link) => await Browser.OpenAsync(link));
        }
    }
}