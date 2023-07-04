using CoRicetta.Data.ViewModels.Recipes;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.RecipeDetailRepo
{
    public interface IRecipeDetailRepo
    {
        Task CreateRecipeDetail(RecipeFormViewModel model, int recipeId);
        Task UpdateRecipeDetail(RecipeFormViewModel model, int recipeId);
    }
}
