using EventsHub.Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventsHub.Mobile.Services.Client
{
    public class TheatreService : ServiceBase
    {
        public async Task<TheatrePlay> GetTheatrePlay(int id)
        {
            if (IsConnected)
            {
                TheatrePlay play = null;
                var json = await httpClient.GetStringAsync($"api/theatre/{id}");
                play = await Task.Run(() => JsonConvert.DeserializeObject<TheatrePlay>(json));

                return play;
            }
            return null;
        }

        public async Task<IEnumerable<TheatrePlay>> GetAllTheatrePlays()
        {
            if (IsConnected)
            {
                IEnumerable<TheatrePlay> plays = null;
                var json = await httpClient.GetStringAsync($"api/theatre");
                plays = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<TheatrePlay>>(json));

                return plays;
            }
            return null;
        }
    }
}
