using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.products.Include(c => c.category).ToList());
        }

        //GET product detail acation method

        public ActionResult Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.products.Include(c => c.category).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        //public ActionResult ProductDetail(int? id)
        //{
        //    List<Product> products = new List<Product>();
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = _db.products.Include(c => c.category).FirstOrDefault(c => c.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    products = HttpContext.Session.Get<List<Products>>("products");
        //    if (products == null)
        //    {
        //        products = new List<Products>();
        //    }
        //    products.Add(product);
        //    HttpContext.Session.Set("products", products);
        //    return RedirectToAction(nameof(Index));
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}