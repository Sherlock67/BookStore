using Book.DataAcess;
using Book.DataAcess.Repository.IRepository;
using Book.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers

{

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType cover)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(cover);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Created Successfully";
                return RedirectToAction("Index");
            }
            return View(cover);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // var catgoryfromdb = _db.GetFirstOrDefault.Find(id);
            var CoverTypeFromDb= _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType cover)
        {
          
            if (ModelState.IsValid)
            {
                //_db.Entry(cat).State = EntityState.Modified;
                _unitOfWork.CoverType.Update(cover);
                _unitOfWork.Save();
                TempData["Edited"] = "Cover Type Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(cover);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CoverType obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return BadRequest();
            }

            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            CoverType obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["delete"] = "Cover Type Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
