using EventsHub.Mobile.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventsHub.Mobile.Services
{
    public class AuthenticationService : ServiceBase
    {
        public async Task<string> Login(Login loginModel)
        {
            string token = "";
            if (IsConnected)
            {
                var serializedItem = JsonConvert.SerializeObject(loginModel);
                var response = await httpClient.PostAsync($"api/account/signin", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                token = await response.Content.ReadAsStringAsync();
            }
            return token;
        }
    }
}
