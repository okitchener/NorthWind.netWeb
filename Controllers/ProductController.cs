using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    public class ProductController(DataContext db) : Controller
    {
        private readonly DataContext _dataContext = db;

        public IActionResult Category(){
            return View(_dataContext.Categories);
        }
    }

}