using EventsHub.BLL.DTO;
using EventsHub.BLL.Interfaces;
using System.Threading.Tasks;

namespace EventsHub.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<string> Login(UserDto userDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserDto> GetUser(LoginDto loginDto)
        {
            throw new System.NotImplementedException();
        }

        public Task Register(RegisterDto registerDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
