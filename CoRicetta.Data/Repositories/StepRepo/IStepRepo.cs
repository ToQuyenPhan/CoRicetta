using CoRicetta.Data.ViewModels.Recipes;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.StepRepo
{
    public interface IStepRepo
    {
        Task CreateSteps(RecipeFormViewModel model, int recipeId);
        Task UpdateSteps(RecipeFormViewModel model, int recipeId);
    }
}
