using EventsHub.Mobile.Constants;
using MonkeyCache.FileStore;
using System;
using System.Net.Http;
using Xamarin.Essentials;

namespace EventsHub.Mobile.Services.Client
{
    public class ServiceBase
    {
        protected HttpClient HttpClient;
        protected bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public ServiceBase()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri($"{AppConstants.API_ENDPOINT}");
        }

        protected bool IsKeyExpired(string url)
        {
            return Barrel.Current.IsExpired(key: url);
        }

        protected string GetToken()
        {
            var url = Api.Login;
            string token = null;
            if (!IsKeyExpired(url))
            {
                token = Barrel.Current.Get<string>(key: url);
            }
            return token;
        }
    }
}
