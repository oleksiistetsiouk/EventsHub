using EventsHub.BLL.DTO;
using EventsHub.BLL.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using EventsHub.BLL.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace EventsHub.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TokenManagement tokenManagement;

        public AuthenticationService(IOptions<TokenManagement> tokenManagement)
        {
            this.tokenManagement = tokenManagement.Value;
        }

        public Task<UserDto> GetUser(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(UserDto userDto)
        {
            return await Task.Run(() =>
            {
                var claim = new[]
                {
                    new Claim("Id", userDto.Id.ToString()),
                    new Claim(ClaimTypes.Email, userDto.Email),
                    new Claim(ClaimTypes.Role, userDto.RoleName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    tokenManagement.Issuer,
                    tokenManagement.Audience,
                    claim,
                    expires: DateTime.Now.AddMinutes(tokenManagement.AccessExpiration),
                    signingCredentials: credentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return token;
            });
        }

        public Task Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
