using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Extensions;
using EventsHub.Mobile.Models;
using EventsHub.Mobile.Services.Client;
using EventsHub.Mobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventsHub.Mobile.ViewModels
{
    public class FilmsViewModel : BaseViewModel
    {
        public ObservableCollection<Film> Films { get; set; }
        public Command LoadFilmsCommand { get; set; }
        public Command FilmTresholdReachedCommand { get; set; }
        public Command RefreshFilmsCommand { get; set; }
        public ObservableCollection<Film> FilteredFilms
        {
            get { return filteredFilms; }
            set { SetProperty(ref filteredFilms, value); }
        }

        private int filmTreshold;
        private bool isRefreshing;
        private int pageNumber;
        private int pagesCount;
        private FilmService filmService;
        private ObservableCollection<Film> filteredFilms;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public int FilmTreshold
        {
            get { return filmTreshold; }
            set { SetProperty(ref filmTreshold, value); }
        }

        public FilmsViewModel()
        {
            Title = "Кіно";
            FilmTreshold = 4;
            pageNumber = 1;
            filmService = new FilmService();
            Films = new ObservableCollection<Film>();
            FilteredFilms = new ObservableCollection<Film>();
            LoadFilmsCommand = new Command(async () => await ExecuteLoadFilmsCommand());
            FilmTresholdReachedCommand = new Command(async () => await FilmsTresholdReached());
            RefreshFilmsCommand = new Command(async () =>
            {
                await ExecuteLoadFilmsCommand();
                pageNumber = 1;
                FilmTreshold = 4;
                IsRefreshing = false;
            });

            MessagingCenter.Subscribe<FilmsPage, TextChangedEventArgs>(this, "FilterFilms", (obj, e) =>
            {
                var filterText = e.NewTextValue;
                var filtered = Films.Where(item => item.Title.ToLower().Contains(filterText.ToLower()));
                if (filtered != null)
                {
                    FilteredFilms = new ObservableCollection<Film>();
                    FilteredFilms.AddRange(filtered);
                }
                else
                {
                    FilteredFilms = new ObservableCollection<Film>();
                }
            });
        }

        async Task FilmsTresholdReached()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                pageNumber++;
                if (pageNumber > pagesCount)
                {
                    FilmTreshold = -1;
                    return;
                }
                var films = await filmService.GetAllFilms(pageNumber);
                FilteredFilms.AddRange(films);
                Films.AddRange(films);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadFilmsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var filmsCount = await filmService.FilmsCount();
                pagesCount = (int)Math.Ceiling((double)filmsCount / AppConstants.PageSize);

                FilmTreshold = 4;
                Films.Clear();
                var films = await filmService.GetAllFilms();
                Films.AddRange(films);
                FilteredFilms.AddRange(films);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
