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
        private FilmService filmService;
        public int PageNumber { get; set; } = 1;
        public int PageCount { get; set; }

        public FilmsViewModel()
        {
            filmService = new FilmService();
            Title = "Films";
            Films = new ObservableCollection<Film>();
            LoadFilmsCommand = new Command(async () => await ExecuteLoadItemsCommand(PageNumber));
        }

        async Task ExecuteLoadItemsCommand(object pageNumber)
        {
            IsBusy = true;
            int pNumber = (int)pageNumber;
            try
            {
                Films.Clear();
                var items = await filmService.GetAllFilms(pNumber);
                foreach (var item in items)
                {
                    Films.Add(item);
                }
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
