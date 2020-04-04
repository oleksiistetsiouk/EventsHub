using EventsHub.BLL.DTO;
using System.Threading.Tasks;

namespace EventsHub.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserDto userDto);
        Task<UserDto> GetUser(LoginDto loginDto);
        Task Register(RegisterDto registerDto);
    }
}
