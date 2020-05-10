using EventsHub.Mobile.Models;

namespace EventsHub.Mobile.ViewModels
{
    public class FilmDetailViewModel : BaseViewModel
    {
        public Film Film { get; set; }
        public FilmDetailViewModel(Film film = null)
        {
            Title = film?.Title;
            Film = film;
        }
    }
}
