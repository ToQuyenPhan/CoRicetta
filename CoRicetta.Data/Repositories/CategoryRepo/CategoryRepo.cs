using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.CategoryRepo
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<List<ViewCategory>> GetCategories()
        {
            var query = from c in context.Categories where c.Status.Equals((int) CategoryStatus.Active) select c;
            List<ViewCategory> items = await query
                                          .Select(selector => new ViewCategory()
                                          {
                                              Id = selector.Id,
                                              CategoryName = selector.CategoryName,
                                              Status = selector.Status
                                          }).ToListAsync();
            return items;
        }

        public async Task CreateCategory(CategoryFormModel model)
        {
            var category = new Category
            {
                CategoryName = model.CategoryName,
                Status = (int)model.Status
            };
            await CreateAsync(category);
        }

        public async Task<bool> IsExitedCategory(CategoryFormModel model)
        {
            var query = from c in context.Categories where c.CategoryName.Equals(model.CategoryName.Trim()) select c;
            var category = await query.Select(selector => new Category()
                                          {
                                              Id = selector.Id,
                                              CategoryName = selector.CategoryName,
                                              Status = selector.Status
                                          }).FirstOrDefaultAsync();
            return (category != null) ? true : false;
        }

        public async Task UpdateCategory(CategoryFormModel model, int categoryId)
        {
            var query = from c in context.Categories where c.Id.Equals(categoryId) select c;
            var category = await query.Select(selector => new Category()
            {
                Id = selector.Id,
                CategoryName = selector.CategoryName,
                Status = selector.Status
            }).FirstOrDefaultAsync();
            category.CategoryName = model.CategoryName;
            category.Status = (int)model.Status;
            await UpdateAsync(category);
        }

        public async Task<ViewCategory> GetCategoryById(int categoryId)
        {
            var query = from c in context.Categories where c.Id.Equals(categoryId) select c;
            ViewCategory item = await query.Select(selector => new ViewCategory()
                                          {
                                              Id = selector.Id,
                                              CategoryName = selector.CategoryName,
                                              Status = selector.Status
                                          }).FirstOrDefaultAsync();
            return (item != null) ? item : null;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var query = from c in context.Categories where c.Id.Equals(categoryId) select c;
            var category = await query.Select(selector => new Category()
            {
                Id = selector.Id,
                CategoryName = selector.CategoryName,
                Status = selector.Status
            }).FirstOrDefaultAsync();
            await DeleteAsync(category);
        }
    }
}
