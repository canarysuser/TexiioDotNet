using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstWebApp.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RoleBasedAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private string _incomingRoles = string.Empty;

        public RoleBasedAuthorizationAttribute(string roles)
        {
            _incomingRoles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            List<string> splitRoles = new List<string>();
            var rolesArray = _incomingRoles.Split(',');
            splitRoles.AddRange(rolesArray);



            var userRole = context.HttpContext.Session?.GetString("RoleName");
            if (userRole is null)
            {
                context.Result = new RedirectToActionResult("Login", "Accounts", null);
            }
            else
            {
                if (splitRoles.FirstOrDefault(c => c.ToLower() == userRole.ToLower()) == null)
                {
                    //context.Result = new JsonResult(new { message = "Unauthorized" })
                    //{
                    //    StatusCode = StatusCodes.Status401Unauthorized
                    //};
                    context.Result = new ViewResult() { ViewName = "UnauthorizedAccess" };
                }
            }
        }
    }
}
