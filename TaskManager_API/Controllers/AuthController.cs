using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager_API.Models;

namespace TaskManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userService.ValidateUser(model.Username, model.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _userService.GenerateToken(model.Username);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginModel model)
        {
            try
            {
                var user = _userService.RegisterUser(model.Username, model.Password);
                var token = _userService.GenerateToken(user.Username);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
