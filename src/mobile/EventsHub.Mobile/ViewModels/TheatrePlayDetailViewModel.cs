using EventsHub.Mobile.Models;

namespace EventsHub.Mobile.ViewModels
{
    public class TheatrePlayDetailViewModel : BaseViewModel
    {
        public TheatrePlay TheatrePlay { get; set; }

        public TheatrePlayDetailViewModel(TheatrePlay play = null)
        {
            Title = play?.Name;
            TheatrePlay = play;
        }
    }
}
