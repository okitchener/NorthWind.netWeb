using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    public class CustomerController(DataContext db) : Controller
    {
        private readonly DataContext _dataContext = db;

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            _dataContext.AddCustomer(customer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Customer> customers = _dataContext.Customers
                .OrderBy(c => c.CompanyName)
                .ToList();

            return View(customers);
        }
    }
}
