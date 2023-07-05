using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.ActionRepo;
using CoRicetta.Data.Repositories.CategoryDetailRepo;
using CoRicetta.Data.Repositories.MenuDetailRepo;
using CoRicetta.Data.Repositories.RecipeDetailRepo;
using CoRicetta.Data.Repositories.RecipeRepo;
using CoRicetta.Data.Repositories.ReportRepo;
using CoRicetta.Data.Repositories.StepRepo;
using CoRicetta.Data.ViewModels.Ingredients;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepo _recipeRepo;
        private IRecipeDetailRepo _recipeDetailRepo;
        private IStepRepo _stepRepo;
        private ICategoryDetailRepo _categoryDetailRepo;
        private IActionRepo _actionRepo;
        private IReportRepo _reportRepo;
        private IMenuDetailRepo _menuDetailRepo;
        private DecodeToken _decodeToken;

        public RecipeService(IRecipeRepo recipeRepo, IRecipeDetailRepo recipeDetailRepo, IStepRepo stepRepo, 
            ICategoryDetailRepo categoryDetailRepo, IActionRepo actionRepo, IReportRepo reportRepo,
            IMenuDetailRepo menuDetailRepo)
        {
            _recipeRepo = recipeRepo;
            _recipeDetailRepo = recipeDetailRepo;
            _stepRepo = stepRepo;
            _categoryDetailRepo = categoryDetailRepo;
            _actionRepo = actionRepo;
            _reportRepo = reportRepo;
            _menuDetailRepo = menuDetailRepo;
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

        public async Task<ViewRecipe> getById(int recipeId)
        {
            try
            {
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

        public async Task CreateRecipe(RecipeFormViewModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            int userId = _decodeToken.Decode(token, "Id");
            var recipeId = await _recipeRepo.CreateRecipe(model, userId);
            await _categoryDetailRepo.CreateCategoryDetail(model, recipeId);
            await _recipeDetailRepo.CreateRecipeDetail(model, recipeId);
            await _stepRepo.CreateSteps(model, recipeId);
        }

        public async Task UpdateRecipe(RecipeFormViewModel model, string token, int recipeId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            var recipe = await _recipeRepo.GetRecipeById(recipeId);
            if (recipe == null) throw new NullReferenceException("Not found any reicpes!");
            await _recipeRepo.UpdateRecipe(model, recipeId);
            await _categoryDetailRepo.UpdateCategoryDetail(model, recipeId);
            await _recipeDetailRepo.UpdateRecipeDetail(model, recipeId);
            await _stepRepo.UpdateSteps(model, recipeId);
        }

        public async Task DeleteRecipe(string token, int recipeId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            var recipe = await _recipeRepo.GetRecipeById(recipeId);
            if (recipe == null) throw new NullReferenceException("Not found any reicpes!");
            await _categoryDetailRepo.DeleteCategoryDetailByRecipeId(recipeId);
            await _recipeDetailRepo.DeleteRecipeDetailByRecipeId(recipeId);
            await _stepRepo.DeleteStepsByRecipeId(recipeId);
            await _actionRepo.DeleteActionsByRecipeId(recipeId);
            await _reportRepo.DeleteReportsByRecipeId(recipeId);
            await _menuDetailRepo.DeleteMenuDetailsByRecipeId(recipeId);
            await _recipeRepo.DeleteRecipe(recipeId);
        }

        public async Task<List<ViewIngredient>> GetShoppingListWithId(int recipeId)
        {
            try
            {
                var recipe = await _recipeRepo.GetRecipeById(recipeId);
                if (recipe == null) throw new NullReferenceException("Not found any recipes!");
                var ingredients = await _recipeRepo.GetIngridientsInRecipe(recipeId);
                if (ingredients == null)
                {
                    throw new ArgumentException("The ingredient does not exist.");
                }
                return ingredients;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Something went wrong, please try again later!");
            }
        }
    }
}
