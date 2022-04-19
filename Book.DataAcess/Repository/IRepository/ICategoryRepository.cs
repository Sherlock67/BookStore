﻿using Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAcess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {

        void Update(CoverType obj);
        void Save();
        void Update(Category cat);
    }
}
