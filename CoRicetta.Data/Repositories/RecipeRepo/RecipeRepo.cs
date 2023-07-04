using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Categories;
using CoRicetta.Data.ViewModels.Ingredients;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using CoRicetta.Data.ViewModels.Steps;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.RecipeRepo
{
    public class RecipeRepo : GenericRepo<Recipe>, IRecipeRepo
    {
        public RecipeRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<PagingResultViewModel<ViewRecipe>> GetRecipes(RecipeFilterRequestModel request)
        {
            var query = from r in context.Recipes
                        join u in context.Users on r.UserId equals u.Id
                        select new { u, r };
            if (request.RecipeStatus.HasValue) query = query.Where(selector => selector.r.Status.Equals(request.RecipeStatus));
            if (request.UserId.HasValue) query = query.Where(selector => selector.r.UserId.Equals(request.UserId));
            if (!string.IsNullOrEmpty(request.RecipeName)) query = query.Where(selector => selector.r.RecipeName.Contains(request.RecipeName));
            if (request.Level.HasValue) query = query.Where(selector => selector.r.Level.Equals(request.Level.ToString()));
            int totalCount = query.Count();
            List<ViewRecipe> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                          .Select(selector => new ViewRecipe()
                                          {
                                              Id = selector.r.Id,
                                              UserId = selector.r.UserId,
                                              UserName = selector.u.UserName,
                                              RecipeName = selector.r.RecipeName,
                                              Level = selector.r.Level,
                                              PrepareTime = selector.r.PrepareTime,
                                              CookTime = selector.r.CookTime,
                                              Image = selector.r.Image,
                                              Description = selector.r.Description,
                                              Status = ((RecipeStatus)selector.r.Status).ToString(),
                                          }
                                          ).ToListAsync();
            foreach(var element in items)
            {
                element.Categories = await GetCategoriesInRecipe(element.Id);
            }
            if (request.CategoryId.HasValue)
            {
                List<ViewRecipe> temp = new List<ViewRecipe>();
                foreach (var recipe in items)
                {
                    foreach(var category in recipe.Categories)
                    {
                        if(category.Id == request.CategoryId)
                        {
                            temp.Add(recipe);
                        }
                    }
                }
                items = temp;
            }
            return (items.Count() > 0) ? new PagingResultViewModel<ViewRecipe>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }

        public async Task<ViewRecipe> GetRecipeById(int recipeId)
        {
            var query = from r in context.Recipes
                        join u in context.Users on r.UserId equals u.Id
                        where r.Id.Equals(recipeId)
                        select new { u, r };
            ViewRecipe item = await query.Select(selector => new ViewRecipe()
                                          {
                                              Id = selector.r.Id,
                                              UserId = selector.r.UserId,
                                              UserName = selector.u.UserName,
                                              RecipeName = selector.r.RecipeName,
                                              Level = selector.r.Level,
                                              PrepareTime = selector.r.PrepareTime,
                                              CookTime = selector.r.CookTime,
                                              Image = selector.r.Image,
                                              Description = selector.r.Description,
                                              Status = ((RecipeStatus)selector.r.Status).ToString(),
                                          }
                                          ).FirstOrDefaultAsync();
            item.Categories = await GetCategoriesInRecipe(item.Id);
            item.Ingredients = await GetIngridientsInRecipe(item.Id);
            item.Steps = await GetStepsInRecipe(item.Id);
            return item;
        }

        public async Task<int> CreateRecipe(RecipeFormViewModel model, int userId)
        {
            var recipe = new Recipe
            {
                UserId = userId,
                RecipeName = model.RecipeName,
                Level = model.Level.ToString(),
                PrepareTime = model.PrepareTime,
                CookTime = model.CookTime,
                Image = model.Image,
                Description = model.Description,
                Status = model.Status
            };
            await CreateAsync(recipe);
            return recipe.Id;
        }

        public async Task UpdateRecipe(RecipeFormViewModel model, int recipeId)
        {
            var query = from r in context.Recipes where r.Id.Equals(recipeId) select r;
            var recipe = await query.Select(selector => new Recipe
            {
                Id = selector.Id,
                UserId = selector.UserId,
                RecipeName = selector.RecipeName,
                Level = selector.Level,
                PrepareTime = selector.PrepareTime,
                CookTime = selector.CookTime,
                Image = selector.Image,
                Description = selector.Description,
                Status = selector.Status
            }).FirstOrDefaultAsync();
            recipe.RecipeName = model.RecipeName;
            recipe.Level = model.Level.ToString();
            recipe.PrepareTime = model.PrepareTime;
            recipe.CookTime = model.CookTime;
            recipe.Image = model.Image;
            recipe.Description = model.Description;
            recipe.Status = model.Status;
            await UpdateAsync(recipe);
        }

        private async Task<List<ViewCategory>> GetCategoriesInRecipe(int recipeId)
        {
            return await (from cd in context.CategoryDetails
                          join c in context.Categories on cd.CategoryId equals c.Id
                          where cd.RecipeId.Equals(recipeId)
                          select new ViewCategory
                          {
                              Id = c.Id,
                              CategoryName = c.CategoryName
                          }).ToListAsync();
        }

        private async Task<List<ViewIngredient>> GetIngridientsInRecipe(int recipeId)
        {
            return await (from rd in context.RecipeDetails
                          join i in context.Ingredients on rd.IngredientId equals i.Id
                          where rd.RecipeId.Equals(recipeId)
                          select new ViewIngredient
                          {
                              Id = i.Id,
                              IngredientName = i.IngredientName,
                              Quantity = rd.Quantity,
                              Measurement = i.Measurement,
                              Calories = i.Calories
                          }).ToListAsync();
        }

        private async Task<List<ViewStep>> GetStepsInRecipe(int recipeId)
        {
            return await (from s in context.Steps
                          where s.RecipeId.Equals(recipeId)
                          select new ViewStep
                          {
                              Id = s.Id,
                              StepNumber = s.StepNumber,
                              Description = s.Description
                          }).ToListAsync();
        }
    }
}
