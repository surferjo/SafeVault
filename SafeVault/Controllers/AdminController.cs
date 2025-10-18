using Microsoft.AspNetCore.Mvc;
using SafeVault.Attributes;

namespace SafeVault.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("dashboard")]
        [Role("admin")]
        public IActionResult Dashboard()
        {
            return Ok("Welcome, Admin!");
        }
    }
}
