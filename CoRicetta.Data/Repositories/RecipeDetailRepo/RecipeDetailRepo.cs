using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Recipes;
using Microsoft.EntityFrameworkCore;
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
                    var isExisted = await CheckSameRecipeDetail(recipeDetail, list);
                    if (isExisted)
                    {
                        list = await UpdateQuantity(recipeDetail, list);
                    }
                    else
                    {
                        list.Add(recipeDetail);
                    }
                }
                await CreateRangeAsync(list);
            }
        }

        public async Task UpdateRecipeDetail(RecipeFormViewModel model, int recipeId)
        {
            if (model.Ingredients.Any() && model.Quantities.Any())
            {
                var query = from rd in context.RecipeDetails where rd.RecipeId.Equals(recipeId) select rd;
                List<RecipeDetail> items = await query.Select(selector => new RecipeDetail
                {
                    RecipeId = selector.RecipeId,
                    IngredientId = selector.IngredientId,
                    Quantity = selector.Quantity
                }).ToListAsync();
                if (items.Count() > 0)
                {
                    await DeleteRangeAsync(items);
                }
                var ingredients = model.Ingredients.Zip(model.Quantities, (i, q) => new { Ingredient = i, Quantity = q });
                List<RecipeDetail> list = new List<RecipeDetail>();
                foreach (var ingredient in ingredients)
                {
                    var recipeDetail = new RecipeDetail
                    {
                        RecipeId = recipeId,
                        IngredientId = ingredient.Ingredient,
                        Quantity = ingredient.Quantity,
                    };
                    var isExisted = await CheckSameRecipeDetail(recipeDetail, list);
                    if (isExisted)
                    {
                        list = await UpdateQuantity(recipeDetail, list);
                    }
                    else
                    {
                        list.Add(recipeDetail);
                    }
                }
                await CreateRangeAsync(list);
            }
        }

        public async Task DeleteRecipeDetailByRecipeId(int recipeId)
        {
            var query = from rd in context.RecipeDetails where rd.RecipeId.Equals(recipeId) select rd;
            List<RecipeDetail> items = await query.Select(selector => new RecipeDetail
            {
                RecipeId = selector.RecipeId,
                IngredientId = selector.IngredientId,
                Quantity = selector.Quantity
            }).ToListAsync();
            if (items.Count() > 0)
            {
                await DeleteRangeAsync(items);
            }
        }

        private async Task<bool> CheckSameRecipeDetail(RecipeDetail recipeDetail, List<RecipeDetail> list)
        {
            var check = false;
            foreach(var item in list)
            {
                if(item.IngredientId.Equals(recipeDetail.IngredientId) 
                        && item.RecipeId.Equals(recipeDetail.RecipeId))
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        private async Task<List<RecipeDetail>> UpdateQuantity(RecipeDetail recipeDetail, List<RecipeDetail> list)
        {
            foreach (var item in list)
            {
                if (item.IngredientId.Equals(recipeDetail.IngredientId)
                        && item.RecipeId.Equals(recipeDetail.RecipeId))
                {
                    item.Quantity += recipeDetail.Quantity;
                    break;
                }
            }
            return list;
        }
    }
}
