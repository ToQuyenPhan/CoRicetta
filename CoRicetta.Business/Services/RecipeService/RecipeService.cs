using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.CategoryDetailRepo;
using CoRicetta.Data.Repositories.RecipeDetailRepo;
using CoRicetta.Data.Repositories.RecipeRepo;
using CoRicetta.Data.Repositories.StepRepo;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using System;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepo _recipeRepo;
        private IRecipeDetailRepo _recipeDetailRepo;
        private IStepRepo _stepRepo;
        private ICategoryDetailRepo _categoryDetailRepo;
        private DecodeToken _decodeToken;

        public RecipeService(IRecipeRepo recipeRepo, IRecipeDetailRepo recipeDetailRepo, IStepRepo stepRepo, 
            ICategoryDetailRepo categoryDetailRepo)
        {
            _recipeRepo = recipeRepo;
            _recipeDetailRepo = recipeDetailRepo;
            _stepRepo = stepRepo;
            _categoryDetailRepo = categoryDetailRepo;
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
    }
}
