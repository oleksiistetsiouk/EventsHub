using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Acr.UserDialogs;
using System;

namespace EventsHub.Mobile.Services.Client
{
    class FilmService : ServiceBase
    {
        public async Task<Film> GetFilm(int id)
        {
            if (IsConnected)
            {
                Film film = null;
                var json = await httpClient.GetStringAsync(string.Format($"{Api.GetFilm}", id));
                film = await Task.Run(() => JsonConvert.DeserializeObject<Film>(json));

                return film;
            }
            return null;
        }

        public async Task<IEnumerable<Film>> GetAllFilms(int pageNumber = 1)
        {
            var url = string.Format($"{Api.GetFilms}", pageNumber, AppConstants.PageSize);
            if (!isKeyExpired(url))
            {
                return Barrel.Current.Get<IEnumerable<Film>>(key: url);
            }

            if (!IsConnected)
            {
                await Task.Yield();
                UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(2));
            }

            var json = await httpClient.GetStringAsync(url);
            var films = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Film>>(json));

            Barrel.Current.Add(key: url, data: films, expireIn: TimeSpan.FromDays(1));

            return films;
        }

        public async Task<int> FilmsCount()
        {
            var url = Api.GetFilmsCount;
            if (!isKeyExpired(url))
            {
                return Barrel.Current.Get<int>(key: url);
            }

            var json = await httpClient.GetStringAsync(Api.GetFilmsCount);
            var count = await Task.Run(() => JsonConvert.DeserializeObject<int>(json));
            Barrel.Current.Add(key: url, data: count, expireIn: TimeSpan.FromDays(1));

            return count;
        }
    }
}
