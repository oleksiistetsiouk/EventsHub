using Acr.UserDialogs;
using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Extensions;
using EventsHub.Mobile.Models;
using EventsHub.Mobile.Services.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventsHub.Mobile.ViewModels
{
    public class FilmsViewModel : BaseViewModel
    {
        public ObservableCollection<Film> Films { get; set; }
        public Command LoadFilmsCommand { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PagesCount { get; set; } = 0;
        private FilmService filmService;

        public FilmsViewModel()
        {
            filmService = new FilmService();
            Title = "Films";
            Films = new ObservableCollection<Film>();
            LoadFilmsCommand = new Command(async () => await ExecuteLoadFilmsCommand(PageNumber));
        }

        private async Task ExecuteLoadFilmsCommand(object pageNumber)
        {
            IsBusy = true;
            try
            {
                if (PagesCount == 0)
                {
                    var filmsCount = await filmService.FilmsCount();
                    PagesCount = (int)Math.Ceiling((double)filmsCount / AppConstants.PageSize);
                }
                if ((int)pageNumber <= PagesCount)
                {
                    var films = await filmService.GetAllFilms((int)pageNumber);
                    Films.AddRange(films);
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
