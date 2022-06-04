using Book.DataAcess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnviroment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _hostEnviroment = hostEnviroment;
        }
        public IActionResult Index()
        {
            //IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.categoryname,
                    Value = i.categoryid.ToString()

                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i=> new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };
            if (id == null || id == 0)
            {
                //create roduct
                
                //ViewBag.CoverTypeList = CoverTypeList;  
                return View(productVM);
            }
            else
            {
                   //update product
                   productVM.product = _unitOfWork.Product.GetFirstOrDefault(u=>u.ProductId == id);
                return View(productVM);
            }
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if(obj.product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl = @"\Images\products\" + fileName + extension;

                }
                if(obj.product.ProductId == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else {
                    _unitOfWork.Product.Update(obj.product);

                }

                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });
            //return View(productList);
        }
       
        [HttpDelete]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            if(obj == null)
            {
                return Json( new {success= false, message = "Cant find the data"});
            }
            var oldImagePath = Path.Combine(_hostEnviroment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new {success = true , message = "Delete Succssful"})
        }
           
    }
            
            
            
}


