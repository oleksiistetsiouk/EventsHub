using EventsHub.Mobile.Models;

namespace EventsHub.Mobile.ViewModels
{
    public class ConcertDetailViewModel : BaseViewModel
    {
        public Concert Concert { get; set; }

        public ConcertDetailViewModel(Concert concert = null)
        {
            Title = concert?.Title;
            Concert = concert;
        }
    }
}
