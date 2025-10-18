using Microsoft.AspNetCore.Mvc;
using SafeVault.Helpers;
using System.Net;

namespace SafeVault.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        [HttpGet("xss-test")]
        public IActionResult XssTest([FromQuery] string input)
        {
            var sanitized = InputValidator.Sanitize(input);
            var htmlEncoded = WebUtility.HtmlEncode(sanitized);

            return Ok(new { sanitized = htmlEncoded });
        }
    }
}
