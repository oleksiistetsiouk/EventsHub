using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EventsHub.Mobile.Models
{
    public class TheatrePlay
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public DateTime Date { get; set; }
        public string PosterUrl { get; set; }
        public string DirectLink { get; set; }
        public string PriceRange => PriceTo == 0 ? PriceFrom.ToString() : $"{PriceFrom}-{PriceTo}";
        public ICommand OpenWebCommand { get; }

        public TheatrePlay()
        {
            OpenWebCommand = new Command<string>(async (string link) => await Browser.OpenAsync(link));
        }
    }
}
