using Book.DataAcess.Repository.IRepository;
using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAcess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Category obj)
        {
            _db.categories.Update(obj);
            //throw new NotImplementedException();
        }

        public void Update(CoverType obj)
        {
            throw new NotImplementedException();
        }
    }
}
