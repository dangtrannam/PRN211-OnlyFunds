﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccess.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(int pageIndex);
        Category GetCategoryById(int id);

    }
}
