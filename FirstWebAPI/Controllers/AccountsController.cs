using FirstWebAPI.Infrastructure;
using FirstWebAPI.Models;
using FirstWebApp.Infrastructure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _authService; 
        private readonly AppSettings _appSettings;

        public AccountsController(
            IUserService authService,
            IOptions<AppSettings> settings
            ) => (_authService, _appSettings) = (authService, settings.Value);

        [HttpGet]
        [Authorization]
        public ActionResult<bool> Verify()
        {
            return true;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate(AuthenticationRequest model)
        {
            var user = _authService.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid credentials. Username/password is wrong." });
            }
            //Create a encryption key from the appsettings 
            var key = Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
            //Create the claims list 
            var claimsList = new List<Claim>
            {
                new Claim("Username", user.Username),
                new Claim("Fullname", user.FullName)
            };
            //create the ClaimsIdentity 
            var claimsIdentity = new ClaimsIdentity(claimsList);
            //Create a SigningCredentials object and choose the SymmetricKey algo 
            var signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(1), //token should expire after one day. 
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler tokenHandler = new(); 
            var preToken = tokenHandler.CreateToken(tokenDescriptor); 
            var token = tokenHandler.WriteToken(preToken);
            var authResponse = new AuthenticationResponse(user: user, token: token);
            return authResponse;
        }
    }
}
