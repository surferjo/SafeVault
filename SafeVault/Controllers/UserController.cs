using Microsoft.AspNetCore.Mvc;
using SafeVault.Data;
using SafeVault.Helpers;

namespace SafeVault.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
