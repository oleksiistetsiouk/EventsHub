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
    public class TheatreService : ServiceBase
    {
        public async Task<IEnumerable<TheatrePlay>> GetAllTheatrePlays(int pageNumber = 1)
        {
            var url = string.Format($"{Api.GetPlays}", pageNumber, AppConstants.PageSize);
            if (!IsKeyExpired(url))
            {
                return Barrel.Current.Get<IEnumerable<TheatrePlay>>(key: url);
            }

            if (!IsConnected)
            {
                await Task.Yield();
                UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(2));
            }

            var json = await httpClient.GetStringAsync(url);
            var plays = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<TheatrePlay>>(json));

            Barrel.Current.Add(key: url, data: plays, expireIn: TimeSpan.FromDays(1));

            return plays;
        }

        public async Task<int> PlaysCount()
        {
            var url = Api.GetPlaysCount;
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
