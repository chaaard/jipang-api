using jipang.Application.DTOs.IN;
using jipang.Application.Interfaces;
using jipang.Application.Services;
using jipang.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jipang_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IJwtService _jwtService;

        public UserAuthController(IUserAuthService userAuthService, IPasswordHashService passwordHashService, IJwtService jwtService)
        {
            _userAuthService = userAuthService;
            _passwordHashService = passwordHashService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserAuthDtoIn userAuthDtoIn)
        {
            var userDto = await _userAuthService.LoginUser(userAuthDtoIn.Username, userAuthDtoIn.Password);

            if (userDto != null)
            {
                return Ok(userDto);
            }
            return NotFound();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(UserAuthDtoIn userAuthDtoIn)
        {
            var userDto = await _userAuthService.LogoutUser(userAuthDtoIn.Username);

            if (userDto != null)
            {
                return Ok(userDto);
            }
            return NotFound();
        }

        [HttpPost("GenerateToken")]
        public Task<IActionResult> GenerateToken(UserDtoIn objUser)
        {
            var token = _jwtService.GenerateToken(objUser);

            if (token != null && token != "")
            {
                return Task.FromResult<IActionResult>(Ok(token));
            }
            return Task.FromResult<IActionResult>(NotFound());
        }

        [HttpPost("GenerateSalt")]
        public Task<IActionResult> GenerateSalt(int salt)
        {
            var strSalt = _passwordHashService.GenerateSalt(salt);

            if (strSalt != null && strSalt != "")
            {
                return Task.FromResult<IActionResult>(Ok(strSalt));
            }
            return Task.FromResult<IActionResult>(NotFound());
        }

        [HttpPost("HashPassword")]
        public Task<IActionResult> HashPassword(string password)
        {
            int saltiness = 70;
            int nIterations = 10101;
            var strSalt = _passwordHashService.GenerateSalt(saltiness);
            var hashedPassword = _passwordHashService.HashPassword(password, strSalt, nIterations, saltiness);

            if (hashedPassword != null && hashedPassword != "")
            {
                return Task.FromResult<IActionResult>(Ok(hashedPassword));
            }
            return Task.FromResult<IActionResult>(NotFound());
        }
    }
}
