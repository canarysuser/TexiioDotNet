using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(viewName: "Index1");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //URL: /home/today
        public string Today() => DateTime.Today.ToString();  //NO UI generated for this.
        //action result can be any .NET object also 

        //URL: /home/nextdate/?days=5  ==> ? is called as a query string
        //does not work without the ? because previously the Model Binder was checking for a parameter named "days" 
        //which was not supplied in the URL. Value passed was being assigned to a parameter named "id" 
        public string NextDate(int days) => DateTime.Now.AddDays(days).ToString();

        //URL: /home/getjson
        public IActionResult GetJson()
        {
            var obj = new { Id = 1234, Name = "Someone", Email = "Someone@example.com" }; 
            return Json(obj);
        }
        //URL: /home/gotohome
        public IActionResult GoToHome() => RedirectToAction("Index");

        //URL: /home/numbertowords/1
        public IActionResult NumberToWords(int id)
        {
            Dictionary<int, string> map = new Dictionary<int, string>
            {
                {1,"One" },{2, "two"}, {3, "three"}, {4, "four"}, {5, "Five"}
            };
            if(map.ContainsKey(id))
            {
                return Json(map[id]);
            } else
            {
                return Content("No matching items found.");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}