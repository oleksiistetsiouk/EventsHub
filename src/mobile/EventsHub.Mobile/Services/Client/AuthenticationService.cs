using Acr.UserDialogs;
using EventsHub.Mobile.Constants;
using EventsHub.Mobile.Models;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventsHub.Mobile.Services.Client
{
    public class AuthenticationService : ServiceBase
    {
        public async Task<string> Login(string email, string password)
        {
            var url = Api.Login;
            string token = null;
            if (!IsKeyExpired(url))
            {
                token = Barrel.Current.Get<string>(key: url);
            }
            else
            {
                if (!IsConnected)
                {
                    await Task.Yield();
                    UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(2));
                }
                else
                {
                    var loginModel = new Login() { Email = email, Password = password };
                    var serializedItem = JsonConvert.SerializeObject(loginModel);

                    var response = await HttpClient.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                    token = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync()).Token;

                    Barrel.Current.Add(key: url, data: token, expireIn: TimeSpan.FromDays(7));
                }
            }
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            return token;
        }
    }

    public class TokenModel
    {
        public string Token { get; set; }
    }
}
