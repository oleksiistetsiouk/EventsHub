using EventsHub.Mobile.Models;
using EventsHub.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheatrePlayDetailPage : ContentPage
    {
        TheatrePlayDetailViewModel viewModel;

        public TheatrePlayDetailPage(TheatrePlayDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public TheatrePlayDetailPage()
        {
            InitializeComponent();

            var play = new TheatrePlay
            {
                Name = "Play 1",
                Description = "This is an film description."
            };

            viewModel = new TheatrePlayDetailViewModel(play);
            BindingContext = viewModel;
        }
    }
}