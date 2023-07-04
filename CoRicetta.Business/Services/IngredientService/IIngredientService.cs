using CoRicetta.Data.ViewModels.Ingredients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.IngredientService
{
    public interface IIngredientService
    {
        Task<List<ViewIngredient>> GetActiveIngredients();
    }
}
