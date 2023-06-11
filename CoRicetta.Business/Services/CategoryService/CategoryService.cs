using CoRicetta.Data.Repositories.CategoryRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
    }
}
