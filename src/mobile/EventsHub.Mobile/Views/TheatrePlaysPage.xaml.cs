using EventsHub.Mobile.Models;
using EventsHub.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheatrePlaysPage : ContentPage
    {
        private TheatrePlaysViewModel viewModel;

        public TheatrePlaysPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TheatrePlaysViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var play = (TheatrePlay)layout.BindingContext;
            await Navigation.PushAsync(new TheatrePlayDetailPage(new TheatrePlayDetailViewModel(play)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.TheatrePlays.Count == 0)
                viewModel.LoadPlaysCommand.Execute(null);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessagingCenter.Send(this, "FilterPlays", e);
        }
    }
}