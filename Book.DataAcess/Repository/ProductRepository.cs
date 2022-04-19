using Book.DataAcess.Repository.IRepository;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAcess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {
            var objfromdb = _db.Products.FirstOrDefault(u=>u.ProductId == obj.ProductId);
            if(objfromdb != null)
            {
                objfromdb.Title = obj.Title;
                objfromdb.ISBN = obj.ISBN;
                objfromdb.Price = obj.Price;
                objfromdb.Price50 = obj.Price50;
                objfromdb.Price100 = obj.Price100;
                objfromdb.ListPrice = obj.ListPrice;
                objfromdb.Description = obj.Description;
                objfromdb.CategoryId = obj.CategoryId;
                objfromdb.Author = obj.Author;
                objfromdb.CoverTypeId = obj.CoverTypeId;
                if(obj.ImageUrl != null)
                {
                    objfromdb.ImageUrl = obj.ImageUrl;
                }
            }
            
          
        }
    }
}
