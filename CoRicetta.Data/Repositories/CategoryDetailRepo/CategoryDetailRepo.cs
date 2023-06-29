using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Recipes;
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
    }
}
