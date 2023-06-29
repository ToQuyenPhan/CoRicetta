using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Recipes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.RecipeDetailRepo
{
    public class RecipeDetailRepo : GenericRepo<RecipeDetail>, IRecipeDetailRepo
    {
        public RecipeDetailRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task CreateRecipeDetail(RecipeFormViewModel model, int recipeId)
        {
            if(model.Ingredients.Any() && model.Quantities.Any())
            {
                var ingredients = model.Ingredients.Zip(model.Quantities, (i, q) => new { Ingredient = i, Quantity = q });
                List<RecipeDetail> list = new List<RecipeDetail>();
                foreach(var ingredient in ingredients)
                {
                    var recipeDetail = new RecipeDetail { 
                        RecipeId = recipeId,
                        IngredientId = ingredient.Ingredient,
                        Quantity = ingredient.Quantity,
                    };
                    list.Add(recipeDetail);
                }
                await CreateRangeAsync(list);
            }
        }
    }
}
