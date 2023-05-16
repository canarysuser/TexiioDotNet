using FirstWebAPI.Models;
using FirstWebApp.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FirstWebAPI.Infrastructure
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings; 
        //private readonly IUserService

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> settings)
            =>(_next, _settings) = (next, settings.Value);

        public async Task Invoke(HttpContext context)
        {
            //get the key from the config file 
            var key = Encoding.UTF8.GetBytes(_settings.AppSecretKey);
            var authHeader = context.Request.Headers["Authorization"]; 
            var authheadervalues = authHeader.FirstOrDefault()?.Split(' ');
            var token = authheadervalues?.Last();
            if(token is not null)
            {
                try
                {
                    JwtSecurityTokenHandler tokenHandler = new();
                    tokenHandler.ValidateToken(
                        token: token,
                        validationParameters: new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero
                        }, out SecurityToken validatedToken);
                    var jwtToken = validatedToken as JwtSecurityToken;
                    var username = jwtToken?.Claims.First(c => c.Type == "Username").Value; 
                    var fullName = jwtToken?.Claims.First(c => c.Type == "Fullname").Value;
                    context.Items["Username"] = username;
                    context.Items["Fullname"] = fullName; 
                } catch (Exception ex) { throw; }
            }
            await _next(context);
        }

    }
}
