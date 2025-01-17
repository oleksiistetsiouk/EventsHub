﻿using EventsHub.Mobile.Models;
using EventsHub.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsHub.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmsPage : ContentPage
    {
        private FilmsViewModel viewModel;

        public FilmsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new FilmsViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var film = (Film)layout.BindingContext;
            await Navigation.PushAsync(new FilmDetailPage(new FilmDetailViewModel(film)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Films.Count == 0)
                viewModel.LoadFilmsCommand.Execute(null);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessagingCenter.Send(this, "FilterFilms", e);
        }
    }
}