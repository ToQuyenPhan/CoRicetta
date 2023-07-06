using CoRicetta.Data.ViewModels.Ingredients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.IngredientRepo
{
    public interface IIngredientRepo
    {
        Task<List<ViewIngredient>> GetActiveIngredients();
        Task UpdateIngredient(IngredientFormModel model, int ingredientId);
        Task<ViewIngredient> GetIngredientById(int ingredientId);
        Task CreateIngredient(IngredientFormModel model);
        Task<bool> IsExistedIngredient(IngredientFormModel model);
        Task DeleteIngredient(int ingredientId);
        Task<List<ViewIngredient>> GetInactiveIngredients();
    }
}
