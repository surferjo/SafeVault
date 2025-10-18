using Microsoft.AspNetCore.Mvc;
using SafeVault.Data;
using SafeVault.Models; // ✅ Only Models.User
using SafeVault.Helpers;

namespace SafeVault.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!InputValidator.IsValidUsername(user.Username) || !InputValidator.IsValidEmail(user.Email))
                return BadRequest("Invalid username or email.");

            bool success = await _userRepository.RegisterUserAsync(user); // ✅ Uses Models.User
            if (!success)
                return Conflict("Username already exists.");

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginRequest)
        {
            var user = await _userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.PasswordHash);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new
            {
                user.Username,
                user.Email,
                user.Role
            });
        }
    }
}
