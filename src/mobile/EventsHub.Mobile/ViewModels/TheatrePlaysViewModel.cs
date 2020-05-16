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
    public class TheatrePlaysViewModel : BaseViewModel
    {
        public ObservableCollection<TheatrePlay> TheatrePlays { get; set; }
        public Command LoadPlaysCommand { get; set; }
        public Command PlaysTresholdReachedCommand { get; set; }
        public Command RefreshPlaysCommand { get; set; }

        private int playTreshold;
        private bool isRefreshing;
        private int pageNumber;
        private int pagesCount;
        private TheatreService theatreService;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public int PlayTreshold
        {
            get { return playTreshold; }
            set { SetProperty(ref playTreshold, value); }
        }

        public TheatrePlaysViewModel()
        {
            Title = "Вистави";
            PlayTreshold = 4;
            pageNumber = 1;
            theatreService = new TheatreService();
            TheatrePlays = new ObservableCollection<TheatrePlay>();
            LoadPlaysCommand = new Command(async () => await ExecuteLoadPlaysCommand());
            PlaysTresholdReachedCommand = new Command(async () => await PlaysTresholdReached());
            RefreshPlaysCommand = new Command(async () =>
            {
                await ExecuteLoadPlaysCommand();
                pageNumber = 1;
                PlayTreshold = 4;
                IsRefreshing = false;
            });
        }

        async Task PlaysTresholdReached()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                pageNumber++;
                if (pageNumber > pagesCount)
                {
                    PlayTreshold = -1;
                    return;
                }
                var plays = await theatreService.GetAllTheatrePlays(pageNumber);
                TheatrePlays.AddRange(plays);
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

        async Task ExecuteLoadPlaysCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var playsCount = await theatreService.PlaysCount();
                pagesCount = (int)Math.Ceiling((double)playsCount / AppConstants.PageSize);

                PlayTreshold = 4;
                TheatrePlays.Clear();
                var plays = await theatreService.GetAllTheatrePlays();
                TheatrePlays.AddRange(plays);
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
