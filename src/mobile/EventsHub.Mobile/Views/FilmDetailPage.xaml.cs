using EventsHub.Mobile.Models;
using EventsHub.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmDetailPage : ContentPage
    {
        FilmDetailViewModel viewModel;

        public FilmDetailPage(FilmDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public FilmDetailPage()
        {
            InitializeComponent();

            var film = new Film
            {
                Title = "Film 1",
                Description = "This is an film description."
            };

            viewModel = new FilmDetailViewModel(film);
            BindingContext = viewModel;
        }
    }
}