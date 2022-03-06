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
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categorylist = _unitOfWork.Category.GetAll();
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
                _unitOfWork.Category.Add(cat);
                _unitOfWork.Save();
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
            var catgoryfromdb = _unitOfWork.Category.GetFirstOrDefault(u => u.categoryid == id);
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
                _unitOfWork.Category.Update(cat);
                _unitOfWork.Save();
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
            Category obj = _unitOfWork.Category.GetFirstOrDefault(u => u.categoryid == id) ;
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
            Category obj = _unitOfWork.Category.GetFirstOrDefault(u => u.categoryid == id); 
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["delete"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
