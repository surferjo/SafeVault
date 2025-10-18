using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SafeVault.Data;
using SafeVault.Models;

namespace SafeVault.Attributes
{
    public class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        public RoleAttribute(string role) => _role = role;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as User;
            if (user == null || user.Role != _role)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
