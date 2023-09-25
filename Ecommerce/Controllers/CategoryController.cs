using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {


        private ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.categories.ToList());
        }

        //GET Create Action Method
        public ActionResult Create()
        {
            return View();
        }


        //POST Create Action Method

        [HttpPost]
        public async Task<IActionResult> Create(Category categoryy)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Add(categoryy);
                await _db.SaveChangesAsync();
                TempData["save"] = "Book type has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(categoryy);
        }

        //GET Edit Action Method

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = _db.categories.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }


        //POST Edit Action Method

        [HttpPost]
        public async Task<IActionResult> Edit(Category categoryy)
        {
            if (ModelState.IsValid)
            {
                _db.Update(categoryy);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoryy);
        }



        //GET Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookType = _db.categories.Find(id);
            if (bookType == null)
            {
                return NotFound();
            }
            return View(bookType);
        }

        //POST Edit Action Method

        [HttpPost]
        public IActionResult Details(Category categoryy)
        {
            return RedirectToAction(nameof(Index));

        }

        //GET Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookType = _db.categories.Find(id);
            if (bookType == null)
            {
                return NotFound();
            }
            return View(bookType);
        }


        //POST Delete Action Method

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, Category categoryy)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != categoryy.Id)
            {
                return NotFound();
            }

            var bookType = _db.categories.Find(id);
            if (bookType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(bookType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoryy);
        }

    }
}
