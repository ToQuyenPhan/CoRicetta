using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.RecipeRepo
{
    public interface IRecipeRepo
    {
        Task<PagingResultViewModel<ViewRecipe>> GetRecipes(RecipeFilterRequestModel request);
        Task<ViewRecipe> GetRecipeById(int recipeId);
        Task<int> CreateRecipe(RecipeFormViewModel model, int userId);
        Task UpdateRecipe(RecipeFormViewModel model, int recipeId);
        Task DeleteRecipe(int recipeId);
    }
}
