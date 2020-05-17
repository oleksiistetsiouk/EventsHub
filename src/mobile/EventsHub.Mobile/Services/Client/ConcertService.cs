using Acr.UserDialogs;
using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Models;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsHub.Mobile.Services.Client
{
    public class ConcertService : ServiceBase
    {
        public async Task<IEnumerable<Concert>> GetAllConcerts(int pageNumber = 1)
        {
            var url = string.Format($"{Api.GetConcerts}", pageNumber, AppConstants.PageSize);
            if (!IsKeyExpired(url))
            {
                return Barrel.Current.Get<IEnumerable<Concert>>(key: url);
            }

            if (!IsConnected)
            {
                await Task.Yield();
                UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(2));
            }

            var json = await httpClient.GetStringAsync(url);
            var concerts = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Concert>>(json));

            Barrel.Current.Add(key: url, data: concerts, expireIn: TimeSpan.FromDays(1));

            return concerts;
        }

        public async Task<int> ConcertsCount()
        {
            var url = Api.GetConcertsCount;
            if (!IsKeyExpired(url))
            {
                return Barrel.Current.Get<int>(key: url);
            }

            var json = await httpClient.GetStringAsync(url);
            var count = await Task.Run(() => JsonConvert.DeserializeObject<int>(json));
            Barrel.Current.Add(key: url, data: count, expireIn: TimeSpan.FromDays(1));

            return count;
        }
    }
}
