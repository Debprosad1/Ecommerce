using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
   
    public class ProductController : Controller
    {

        private ApplicationDbContext _db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _he;

        public ProductController(ApplicationDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.products.Include(a=>a.category).ToList());
        }

        //Get Create method
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_db.categories.ToList(), "Id", "category");
            return View();
        }

        //Post Create method
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            //if (ModelState.IsValid)
            //{
                var searchProduct = _db.products.FirstOrDefault(c => c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This book is already exist";
                    ViewData["CategoryId"] = new SelectList(_db.categories.ToList(), "Id", "category");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                _db.products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}

            //return View(product);
        }

        //GET Edit Action Method

        public ActionResult Edit(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_db.categories.ToList(), "Id", "category");
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.products.Include(c => c.category)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        //POST Edit Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {
            //if (ModelState.IsValid)
            //{
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                _db.products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}

            //return View(product);
        }


        //GET Delete Action Method
        public ActionResult Delete(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_db.categories.ToList(), "Id", "category");

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.products.Include(c => c.category).Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        //POST Delete Action Method

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _db.products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
