using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    //URL: /statemanager/<actions>
    public class StateManagerController : Controller
    {
        public IActionResult Index(int id = 0)
        {
            ViewData["Message"] = "This is ViewData Message";
            ViewBag.MessageBag = "This is ViewBag message";
            TempData["Message"] = "This is TempData Message";
            HttpContext.Session.SetString("Message", "Session Message is set here.");
            if(id==0)
                return View();
            else 
                return RedirectToAction(nameof(SubAction));
        }
        public IActionResult SubAction()
        {
            return View();
        }
    }
}
