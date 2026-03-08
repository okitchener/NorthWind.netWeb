using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Controllers
{
    public class ProductController(DataContext db) : Controller
    {
        private readonly DataContext _dataContext = db;

        public IActionResult Category(){
            List<Category> categories = _dataContext.Categories
                .OrderBy(c => c.CategoryName)
                .ToList();

            return View(categories);
        }

        public IActionResult Index(int id)
        {
            Category? category = _dataContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null)
            {
                return NotFound();
            }

            ViewBag.CategoryName = category.CategoryName;
            List<Product> products = _dataContext.Products
                .Where(p => p.CategoryId == id && !p.Discontinued)
                .OrderBy(p => p.ProductName)
                .ToList();

            return View(products);
        }
    }

}