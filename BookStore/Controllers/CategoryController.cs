using Book.DataAcess;
using Book.DataAcess.Repository.IRepository;
using Book.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categorylist = _db.GetAll();
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
                _db.Add(cat);
                _db.Save();
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
            // var catgoryfromdb = _db.GetFirstOrDefault.Find(id);
            var catgoryfromdb = _db.GetFirstOrDefault(u => u.categoryid == id);
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
                //_db.Entry(cat).State = EntityState.Modified;
                _db.Update(cat);
                _db.Save();
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
            Category obj = _db.GetFirstOrDefault(u => u.categoryid == id) ;
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
            Category obj = _db.GetFirstOrDefault(u => u.categoryid == id); 
            
            _db.Remove(obj);
            _db.Save();
            TempData["delete"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }
        

    }
}
