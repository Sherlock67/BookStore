using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categorylist = _db.categories.ToList(); 
            return View(categorylist);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            if(cat.categoryname == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Display can not exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.categories.Add(cat);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var catgoryfromdb = _db.categories.Find(id);
            if(catgoryfromdb == null)
            {
                return NotFound();
            }
            return View(catgoryfromdb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat)
        {
            if (cat.categoryname == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Display can not exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.categories.Update(cat);
                _db.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catgoryfromdb = _db.categories.Find(id);
            if (catgoryfromdb == null)
            {
                return NotFound();
            }
            return View(catgoryfromdb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Delete Successfully";
            return RedirectToAction("Index");
        }

    }
}
