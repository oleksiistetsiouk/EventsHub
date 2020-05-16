using EventsHub.Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventsHub.Mobile.Services.Client
{
    public class TheatreService : ServiceBase
    {
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
