using Microsoft.AspNetCore.Http;
using SafeVault.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SafeVault.Middleware
{
    public class FakeAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public FakeAuthMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, SafeVaultContext db)
        {
            if (context.Request.Headers.TryGetValue("Username", out var username))
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                    context.Items["User"] = user;
            }

            await _next(context);
        }
    }
}
