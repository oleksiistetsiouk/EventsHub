﻿using MonkeyCache.FileStore;
using System;
using System.Net.Http;
using Xamarin.Essentials;

namespace EventsHub.Mobile.Services.Client
{
    public class ServiceBase
    {
        protected HttpClient httpClient;
        protected bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public ServiceBase()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri($"{App.ApiUrl}");
        }

        protected bool isKeyExpired(string url)
        {
            return Barrel.Current.IsExpired(key: url);
        }
    }
}
