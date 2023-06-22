using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.RecipeRepo;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using CoRicetta.Data.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepo _recipeRepo;
        private DecodeToken _decodeToken;

        public RecipeService(IRecipeRepo recipeRepo)
        {
            _recipeRepo = recipeRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task<PagingResultViewModel<ViewRecipe>> GetRecipes(string token, PagingRequestViewModel request)
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
    }
}
