using EventsHub.Mobile.Models;
using EventsHub.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConcertsPage : ContentPage
    {
        private ConcertsViewModel viewModel;

        public ConcertsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ConcertsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var concert = (Concert)layout.BindingContext;
            await Navigation.PushAsync(new ConcertDetailPage(new ConcertDetailViewModel(concert)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Concerts.Count == 0)
                viewModel.LoadConcertsCommand.Execute(null);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessagingCenter.Send(this, "FilterConcerts", e);
        }
    }
}