using CoRicetta.Business.Utils;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Repositories.RecipeRepo;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using System;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepo _recipeRepo;
        private DecodeToken _decodeToken;
        private IGenericRepo<Recipe> _genericRepository;

        public RecipeService(IRecipeRepo recipeRepo, IGenericRepo<Recipe> genericRepository)
        {
            _recipeRepo = recipeRepo;
            _genericRepository = genericRepository;
            _decodeToken = new DecodeToken();
        }

        public async Task<PagingResultViewModel<ViewRecipe>> GetRecipes(string token, RecipeFilterRequestModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            PagingResultViewModel<ViewRecipe> recipes = await _recipeRepo.GetRecipes(request);
            if (recipes.Items == null) throw new NullReferenceException("Not found any recipes");
            foreach(var recipe in recipes.Items)
            {
                switch (recipe.Level.ToString())
                {
                    case "Easy":
                        recipe.Level = "Dễ";
                        break;
                    case "Normal":
                        recipe.Level = "Trung bình";
                        break;
                    case "Hard":
                        recipe.Level = "Khó";
                        break;
                    default:
                        break;
                }
            }
            return recipes;
        }

        //public async Task<IList<Recipe>> getWithFilters(int? userId, string? name, int? categoryId, Level? level)
        //{
        //    try
        //    {
        //        // var cats = CategoryDetail
        //        if (level == Level.Easy)
        //        {
        //            var recipes = await _genericRepository.WhereAsync(r =>
        //                (userId == 0 || r.UserId == userId) &&
        //                (categoryId == 0 || r..Any(c => c.CategoryId == categoryId)) &&
        //                (r.Level.Equals("1")),
        //                "User");
        //            return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();
        //        }
        //        else if (level == eLevel.NORMAL)
        //        {
        //            var recipes = await _recipeRepository.WhereAsync(r =>
        //               (userId == 0 || r.UserId == userId) &&
        //               (categoryId == 0 || r.CategoryDetails.Any(c => c.CategoryId == categoryId)) &&
        //               (r.Level.Equals("2")),
        //               "User");
        //            return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();
        //        }
        //        else if (level == eLevel.HARD)
        //        {
        //            var recipes = await _recipeRepository.WhereAsync(r =>
        //               (userId == 0 || r.UserId == userId) &&
        //               (categoryId == 0 || r.CategoryDetails.Any(c => c.CategoryId == categoryId)) &&
        //               (r.Level.Equals("3")),
        //               "User");
        //            return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();
        //        }
        //        else
        //        {
        //            var recipes = await _recipeRepository.WhereAsync(r =>
        //               (userId == 0 || r.UserId == userId) &&
        //               (categoryId == 0 || r.CategoryDetails.Any(c => c.CategoryId == categoryId)) /*&&
        //               (level == eLevel.ALL)*/,
        //               "User");
        //            return recipes.Where(r => (name == null || StringUtils.removeDiacritics(r.RecipeName).Contains(StringUtils.removeDiacritics(name)))).ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw new ArgumentException("Something went wrong, please try again later!");
        //    }
        //}

        public async Task<ViewRecipe> getById(string token, int recipeId)
        {
            try
            {
                string role = _decodeToken.DecodeText(token, "Role");
                if (role.Equals("ADMIN"))
                {
                    throw new UnauthorizedAccessException("You do not have permission to access this resource!");
                }
                var recipe = await _recipeRepo.GetRecipeById(recipeId);
                if (recipe == null)
                {
                    throw new ArgumentException("The recipe does not exist.");
                }
                return recipe;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Something went wrong, please try again later!");
            }
        }
    }
}
