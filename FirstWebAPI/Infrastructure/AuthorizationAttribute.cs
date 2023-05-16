using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstWebAPI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class, AllowMultiple =true)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var username = context.HttpContext?.Items["Username"]?.ToString();
            if(string.IsNullOrEmpty(username) )
            {
                context.Result = new JsonResult(new { message = "Unauthorized." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
