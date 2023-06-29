using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<PagingResultViewModel<ViewRecipe>> GetRecipes(string token, RecipeFilterRequestModel request);
        Task<ViewRecipe> getById(string token, int recipeId);
        Task CreateRecipe(RecipeFormViewModel model, string token);
    }
}
