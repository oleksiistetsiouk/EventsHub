using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            if (IsConnected)
            {
                IEnumerable<Film> films = null;
                var json = await httpClient.GetStringAsync(string.Format($"{Api.GetFilms}", pageNumber, Api.PageSize));
                films = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Film>>(json));

                return films;
            }
            return null;
        }
    }
}
