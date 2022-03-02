using Book.DataAcess;
using Book.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
            //var catgoryfromdb = _db.categories.FirstOrDefault(u => u.categoryname == "categoryid");
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
                _db.Entry(cat).State = EntityState.Modified;
                //_db.categories.Update(cat);
                _db.SaveChanges();
                TempData["Edited"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Category obj  = _db.categories.Find(id);
            if(obj == null)
            {
                return BadRequest();
            }

            return View(obj);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            Category obj = _db.categories.Find(id);
            
            _db.categories.Remove(obj);
            _db.SaveChanges();
            TempData["delete"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }
        

    }
}
