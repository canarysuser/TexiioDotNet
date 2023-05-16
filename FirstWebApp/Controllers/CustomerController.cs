using FirstWebApp.Infrastructure;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    
    public class CustomerController : Controller
    {
        IRepository<Customer, string> _repository;
        public CustomerController(IRepository<Customer, string> repository)
        {
            //_repository = new CustomerListRepository();
            _repository = repository;
        }

        public IActionResult List()
        {
            var model = _repository.GetAll();
            return View(model);
        }

        public IActionResult Details(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id", "The Id cannot be blank or empty.");
            }
            var model = _repository.Get(id);
            if(model!=null)
                return View(model);
            else 
                return NotFound();
        }
        // [Authorize]
        [RoleBasedAuthorization("Operators,Administrator")]
        public IActionResult Create()
        {
            var model = new Customer();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection collection) 
        {
            var model = new Customer();
            model.CustomerId = collection["CustomerId"].ToString();
            model.CompanyName = collection["CompanyName"].ToString();
            model.ContactName = collection["ContactName"].ToString();
            model.City= collection["City"].ToString();
            model.Country = collection["Country"].ToString();

            _repository.Create(model);

            return RedirectToAction("List");
        }
        //[Authorize]
        [RoleBasedAuthorization("Administrator")]
        public IActionResult Edit(string id)
        {
            var model = _repository.Get(id);
            if (model is not null)
                return View(model);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(string id, Customer model)
        {
            if(!ModelState.IsValid)
                return View(model);

            _repository.Update(model);

            return RedirectToAction("List");
        }
        //[Authorize]
        [RoleBasedAuthorization("Operators")]
        public IActionResult Delete(string id)
        {
            var model = _repository.Get(id);
            if (model is not null)
                return View(model);
            else
                return NotFound();
        }


        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConfirmed(string id) 
        {
            _repository.Delete(id);
            return RedirectToAction("List");
        }

        public IActionResult Index()
        {
            Customer customer = new Customer
            {
                CustomerId = "12345",
                CompanyName = "ASP.NET MVC Pvt Ltd.",
                ContactName = "MS",
                City = "Bengaluru",
                Country = "India"
            };
            ViewData["PageTitle"] = "Customer Details";
            ViewBag.Description = "View the customer details for customer -  " + customer.CompanyName;
            return View(model: customer);
        }

    }
}
