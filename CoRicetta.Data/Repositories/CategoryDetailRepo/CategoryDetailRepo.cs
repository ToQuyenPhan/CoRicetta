using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Recipes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.CategoryDetailRepo
{
    public class CategoryDetailRepo : GenericRepo<CategoryDetail>, ICategoryDetailRepo
    {
        public CategoryDetailRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task CreateCategoryDetail(RecipeFormViewModel model, int recipeId)
        {
            if(model.Categories.Any())
            {
                List<CategoryDetail> list = new List<CategoryDetail>();
                foreach(var category in model.Categories)
                {
                    var categoryDetail = new CategoryDetail {
                        RecipeId = recipeId,
                        CategoryId = category
                    };
                    list.Add(categoryDetail);
                }
                await CreateRangeAsync(list);
            }
        }

        public async Task UpdateCategoryDetail(RecipeFormViewModel model, int recipeId)
        {
            if (model.Categories.Any())
            {
                var query = from cd in context.CategoryDetails where cd.RecipeId.Equals(recipeId) select cd;
                List<CategoryDetail> items = await query.Select(selector => new CategoryDetail
                {
                    RecipeId = selector.RecipeId,
                    CategoryId = selector.CategoryId
                }).ToListAsync();
                if(items.Count() > 0)
                {
                    await DeleteRangeAsync(items);
                }
                List<CategoryDetail> list = new List<CategoryDetail>();
                foreach (var category in model.Categories)
                {
                    var categoryDetail = new CategoryDetail
                    {
                        RecipeId = recipeId,
                        CategoryId = category
                    };
                    list.Add(categoryDetail);
                }
                await CreateRangeAsync(list);
            }
        }
    }
}
