using EventsHub.Mobile.Models;
using EventsHub.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConcertDetailPage : ContentPage
    {
        ConcertDetailViewModel viewModel;

        public ConcertDetailPage(ConcertDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ConcertDetailPage()
        {
            InitializeComponent();

            var concert = new Concert
            {
                Title = "Concert 1",
                Description = "This is an film description."
            };

            viewModel = new ConcertDetailViewModel(concert);
            BindingContext = viewModel;
        }
    }
}