using CoRicetta.Business.Utils;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Repositories.RecipeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly IGenericRepo<Recipe> _recipeRepository;

        public RecipeService(IGenericRepo<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IList<Recipe>> getWithFilters(int? userId, string? name, int? categoryId, eLevel? level)
        {
            try
            {
               // var cats = CategoryDetail
                if (level == eLevel.EASY)
                {
                    var recipes = await _recipeRepository.WhereAsync(r =>
                        (userId == 0 || r.UserId == userId) &&
                        (categoryId == 0 || r.CategoryDetails.Any(c => c.CategoryId == categoryId)) &&
                        (r.Level.Equals("1")),
                        "User");
                    return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();
                }else if (level == eLevel.NORMAL)
                {
                    var recipes = await _recipeRepository.WhereAsync(r =>
                       (userId == 0 || r.UserId == userId) &&
                       (categoryId == 0 || r.CategoryDetails.Any(c => c.CategoryId == categoryId)) &&
                       (r.Level.Equals("2")),
                       "User");
                    return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();
                }
                else if(level == eLevel.HARD)
                {
                    var recipes = await _recipeRepository.WhereAsync(r =>
                       (userId == 0 || r.UserId == userId) &&
                       (categoryId == 0 || r.CategoryDetails.Any(c => c.CategoryId == categoryId)) &&
                       (r.Level.Equals("3")),
                       "User");
                    return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();
                }
                else
                {
                    var recipes = await _recipeRepository.WhereAsync(r =>
                       (userId == 0 || r.UserId == userId) &&
                       (categoryId == 0 || r.CategoryDetails.Any(c => c.CategoryId == categoryId)) /*&&
                       (level == eLevel.ALL)*/,
                       "User");
                    return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Something went wrong, please try again later!");
            }
        }

        public async Task<Recipe> getById(int recipeId)
        {
            try
            {
                var recipe = await _recipeRepository.WhereAsync(r => r.Id == recipeId, /*"Ingredients",*/"Actions", "Steps", "User"/*, "CategoryDetails.Category"*/);

                if (recipe == null)
                {
                    throw new ArgumentException("The recipe does not exist.");
                }
                return recipe.First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Something went wrong, please try again later!");
            }
        }
    }
}


