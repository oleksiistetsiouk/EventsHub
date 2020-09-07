using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EventsHub.Mobile.Models
{
    public class Concert
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string PosterUrl { get; set; }
        public string DirectLink { get; set; }
        public string Place { get; set; }
        public bool IsShown => DateTime.Now > Date;

        public ICommand OpenWebCommand { get; }

        public Concert()
        {
            OpenWebCommand = new Command<string>(async (string link) => await Browser.OpenAsync(link));
        }
    }
}
