using CoRicetta.Data.ViewModels.Ingredients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.IngredientService
{
    public interface IIngredientService
    {
        Task<List<ViewIngredient>> GetActiveIngredients();
        Task UpdateIngredient(IngredientFormModel model, string token, int ingredientId);
        Task<ViewIngredient> GetIngredientById(string token, int ingredientId);
        Task CreateIngredient(IngredientFormModel model);
        Task DeleteIngredient(int ingredientId, string token);
        Task<List<ViewIngredient>> GetInactiveIngredients(string token);
    }
}
