using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class ProductApiFEController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5042");
                var response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    ViewBag.Products = result;
                    ViewBag.Message = "Success";
                }
                else
                {
                    ViewBag.Products = null;
                    ViewBag.Message = "Nothing retrieved.";
                }
            }
            return View();
        }

        public  IActionResult List()
        {
            return View();
        }
    }
}
