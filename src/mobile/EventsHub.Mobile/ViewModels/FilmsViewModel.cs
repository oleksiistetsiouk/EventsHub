using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Extensions;
using EventsHub.Mobile.Models;
using EventsHub.Mobile.Services.Client;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private int filmTreshold;
        private bool isRefreshing;
        private int pageNumber;
        private int pagesCount;
        private FilmService filmService;

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
            LoadFilmsCommand = new Command(async () => await ExecuteLoadFilmsCommand());
            FilmTresholdReachedCommand = new Command(async () => await FilmsTresholdReached());
            RefreshFilmsCommand = new Command(async () =>
            {
                await ExecuteLoadFilmsCommand();
                pageNumber = 1;
                FilmTreshold = 4;
                IsRefreshing = false;
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
