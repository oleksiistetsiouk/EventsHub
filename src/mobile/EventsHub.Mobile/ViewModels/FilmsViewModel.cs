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
        private int _filmTreshold;
        private bool _isRefreshing;
        public int PageNumber { get; set; } = 1;
        public int PagesCount { get; set; } = 0;
        private FilmService filmService;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public int FilmTreshold
        {
            get { return _filmTreshold; }
            set { SetProperty(ref _filmTreshold, value); }
        }

        public FilmsViewModel()
        {
            FilmTreshold = 4;
            filmService = new FilmService();
            Title = "Films";
            Films = new ObservableCollection<Film>();
            LoadFilmsCommand = new Command(async () => await ExecuteLoadFilmsCommand());
            FilmTresholdReachedCommand = new Command(async () => await FilmsTresholdReached());
            RefreshFilmsCommand = new Command(async () =>
            {
                await ExecuteLoadFilmsCommand();
                PageNumber = 1;
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
                PageNumber++;
                if (PageNumber > PagesCount)
                {
                    FilmTreshold = -1;
                    return;
                }
                var films = await filmService.GetAllFilms(PageNumber);
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
                PagesCount = (int)Math.Ceiling((double)filmsCount / AppConstants.PageSize);

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
