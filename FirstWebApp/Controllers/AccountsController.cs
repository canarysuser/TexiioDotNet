using FirstWebApp.Infrastructure;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FirstWebApp.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //URL: /accounts/basiclogin
        public IActionResult BasicLogin()
        {
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "User1"),
                new Claim("Password", "PWD")
            };
            //Define the Authentication Scheme -- Use the CookieAuthenticationScheme 
            var cookieAuthScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //create the identity of the user based on the above claim set 
            var identity = new ClaimsIdentity(
                claims: claimsList,
                authenticationType: cookieAuthScheme);
            //build the principal object based on the set of Identities 
            var principal = new ClaimsPrincipal(identity: identity);
            //ASp.NET will generate a response cookie by using the Identity/Principal and push it onto the 
            //response stream. 
            HttpContext.SignInAsync(
                scheme: cookieAuthScheme, 
                principal: principal,
                properties: new AuthenticationProperties { IsPersistent=false }
                ).GetAwaiter().GetResult();

            HttpContext.Session.SetString("IsAuthenticated", "true");

            return RedirectToAction("Index");
        }

        public IActionResult BasicLogout()
        {
            HttpContext.SignOutAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme
                ).GetAwaiter()
                .GetResult();

            HttpContext.Session.Clear(); 
            return RedirectToAction("Index");
        }


        public IActionResult Login()
        {
            var model = new LoginViewModel(); 
            return View(model);
        }
        IUserService _authService; 
        public AccountsController(
            IUserService authService
            ) 
            => _authService = authService;

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            //varify the credentials against the DB 
            var user = _authService.Authenticate(model.Username, model.Password); 
            if(user is null)
            {
                ModelState.AddModelError("", "Bad Username/password."); 
                return View(model);
            }
            var role = _authService.GetRole(user.UserId);
            //If the user exists: Claims, Identity, Principal and SignInAsync
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim("Password", model.Password),
                new Claim(ClaimTypes.Role, role.RoleName)
            };
            var cookieAuthScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var identity = new ClaimsIdentity(
                claims: claimsList,
                authenticationType: cookieAuthScheme);
            var principal = new ClaimsPrincipal(identity: identity);
            await HttpContext.SignInAsync(
                scheme: cookieAuthScheme,
                principal: principal,
                properties: new AuthenticationProperties { IsPersistent = false }
                );

            HttpContext.Session.SetString("Username", user.FullName);
            HttpContext.Session.SetString("RoleName", role.RoleName);
            HttpContext.Items.Add("User", user);
            HttpContext.Items.Add("Role", role);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme
                );
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
