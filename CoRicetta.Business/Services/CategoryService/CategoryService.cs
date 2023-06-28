using CoRicetta.Data.Repositories.CategoryRepo;
using CoRicetta.Data.ViewModels.Categories;
using System;
using System.Collections.Generic;
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

        public async Task<List<ViewCategory>> GetCategories()
        {
            List<ViewCategory> categories = await _categoryRepo.GetCategories();
            if (categories == null) throw new NullReferenceException("Not found any categories");
            return categories;
        }
    }
}
