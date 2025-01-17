﻿using EventsHub.BLL.DTO;
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
using EventsHub.DAL.Entities;
using EventsHub.Common.Helpers;
using AutoMapper;
using EventsHub.Common;
using Common.Exceptions;
using EventsHub.Common.Exceptions;

namespace EventsHub.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TokenManagement tokenManagement;
        private readonly UnitOfWork unitOfWork;

        public AuthenticationService(IOptions<TokenManagement> tokenManagement, UnitOfWork unitOfWork)
        {
            this.tokenManagement = tokenManagement.Value;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetUser(LoginDto loginDto)
        {
            var user = await unitOfWork.UserRepository.Get(u => u.Email == loginDto.Email);
            if (user == null)
                throw new NotFoundException($"User with {nameof(loginDto.Email)} {loginDto.Email}");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()
                .ForMember(u => u.RoleName, opt => opt.MapFrom(u => u.Role.Name)))
                .CreateMapper();
            var userDto = mapper.Map<User, UserDto>(user);

            if (PasswordHasher.VerifyHashedPassword(user.PasswordHash, loginDto.Password))
            {
                return userDto;
            }
            throw new AuthorizationException($"Invalid password");
        }

        public async Task<string> Login(UserDto userDto)
        {
            return await Task.Run(() =>
            {
                var claim = new[]
                {
                    new Claim("Id", userDto.UserId.ToString()),
                    new Claim(ClaimTypes.Email, userDto.Email),
                    new Claim(ClaimTypes.Role, userDto.RoleName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    tokenManagement.Issuer,
                    tokenManagement.Audience,
                    claim,
                    expires: (userDto.RoleName == Roles.MobileClient) ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(tokenManagement.AccessExpiration),
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
                RoleId = (await unitOfWork.Repository<Role>().Get(r => r.Name == "User")).RoleId
            };

            unitOfWork.Repository<User>().Add(newUser);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
