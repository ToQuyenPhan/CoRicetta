using CoRicetta.Data.ViewModels.Recipes;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.CategoryDetailRepo
{
    public interface ICategoryDetailRepo
    {
        Task CreateCategoryDetail(RecipeFormViewModel model, int recipeId);
        Task UpdateCategoryDetail(RecipeFormViewModel model, int recipeId);
    }
}
