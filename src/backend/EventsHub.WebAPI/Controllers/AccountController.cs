using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsHub.BLL.DTO;
using EventsHub.BLL.Interfaces;
using EventsHub.WebAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsHub.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel loginModel)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<LoginViewModel, LoginDto>()).CreateMapper();
                var loginDto = mapper.Map<LoginViewModel, LoginDto>(loginModel);

                var user = await authenticationService.GetUser(loginDto);
                var token = await authenticationService.Login(user);
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Invalid Request");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] RegisterViewModel newUser)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, RegisterDto>()).CreateMapper();
            var newUserDto = mapper.Map<RegisterViewModel, RegisterDto>(newUser);

            await authenticationService.Register(newUserDto);

            return Ok();
        }
    }
}