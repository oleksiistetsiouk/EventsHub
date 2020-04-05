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
using EventsHub.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using EventsHub.DAL.Entities;
using EventsHub.Common.Helpers;
using AutoMapper;

namespace EventsHub.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TokenManagement tokenManagement;
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationService(IOptions<TokenManagement> tokenManagement, IUnitOfWork unitOfWork)
        {
            this.tokenManagement = tokenManagement.Value;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetUser(LoginDto loginDto)
        {
            var user = await unitOfWork.Repository<User>().GetAll().Include(x => x.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()
                .ForMember(u => u.RoleName, opt => opt.MapFrom(u => u.Role.Name)))
                .CreateMapper();
            var userDto = mapper.Map<User, UserDto>(user);

            if (PasswordHasher.VerifyHashedPassword(user.PasswordHash, loginDto.Password))
            {
                return userDto;
            }
            throw new Exception($"Cannot get user with {nameof(loginDto.Email)}: {loginDto.Email}.");
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

        public async Task Register(RegisterDto newUserDto)
        {
            var newUser = new User()
            {
                Email = newUserDto.Email,
                PasswordHash = PasswordHasher.HashPassword(newUserDto.Password),
                RoleId = (await unitOfWork.Repository<Role>().Get(r => r.Name == "User")).Id
            };

            unitOfWork.Repository<User>().Add(newUser);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
