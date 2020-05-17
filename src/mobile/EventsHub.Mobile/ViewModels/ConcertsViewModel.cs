﻿using EventsHub.Mobile.Constants;
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
    public class ConcertsViewModel : BaseViewModel
    {
        public ObservableCollection<Concert> Concerts { get; set; }
        public Command LoadConcertsCommand { get; set; }
        public Command ConcertTresholdReachedCommand { get; set; }
        public Command RefreshConcertsCommand { get; set; }

        private int concertTreshold;
        private bool isRefreshing;
        private int pageNumber;
        private int pagesCount;
        private ConcertService concertService;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public int ConcertTreshold
        {
            get { return concertTreshold; }
            set { SetProperty(ref concertTreshold, value); }
        }

        public ConcertsViewModel()
        {
            Title = "Концерти";
            ConcertTreshold = 4;
            pageNumber = 1;
            concertService = new ConcertService();
            Concerts = new ObservableCollection<Concert>();
            LoadConcertsCommand = new Command(async () => await ExecuteLoadConcertsCommand());
            ConcertTresholdReachedCommand = new Command(async () => await ConcertsTresholdReached());
            RefreshConcertsCommand = new Command(async () =>
            {
                await ExecuteLoadConcertsCommand();
                pageNumber = 1;
                ConcertTreshold = 4;
                IsRefreshing = false;
            });
        }

        async Task ConcertsTresholdReached()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                pageNumber++;
                if (pageNumber > pagesCount)
                {
                    ConcertTreshold = -1;
                    return;
                }
                var concerts = await concertService.GetAllConcerts(pageNumber);
                Concerts.AddRange(concerts);
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

        async Task ExecuteLoadConcertsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var concertsCount = await concertService.ConcertsCount();
                pagesCount = (int)Math.Ceiling((double)concertsCount / AppConstants.PageSize);

                ConcertTreshold = 4;
                Concerts.Clear();
                var concerts = await concertService.GetAllConcerts();
                Concerts.AddRange(concerts);
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