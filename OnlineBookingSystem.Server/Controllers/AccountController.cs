using Microsoft.AspNetCore.Mvc;
using OnlineBookingSystem.Server.Dtos;
using OnlineBookingSystem.Server.Models;
using OnlineBookingSystem.Server.Services;

namespace OnlineBookingSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.UserName = userRegisterDto.UserName.ToLower();

            if (await _authService.UserExists(userRegisterDto.UserName))
                return BadRequest("UserName is already taken");

            var userToCreate = new User
            {
                UserName = userRegisterDto.UserName,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                Phone = userRegisterDto.Phone,
            };

            var createdUser = await _authService.Register(userToCreate, userRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            Tuple<string?, User> temp = await _authService.Login(userLoginDto.Username.ToLower(), userLoginDto.Password);

            if (temp == null || temp.Item1 == null || temp.Item2 == null) 
                return Unauthorized();

            var user = temp.Item2;
            var userDto = new 
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
            };

            return Ok(new 
            { 
                token = temp.Item1,
                user = userDto
            });
        }
    }

}
