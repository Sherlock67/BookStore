using Book.DataAcess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            if(id == null || id == 0)
            {
                //create product
                return View(product);
            }
            else
            {
                //update product
            }
            return View(product);
        }
    }
}
