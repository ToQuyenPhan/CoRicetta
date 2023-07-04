using CoRicetta.Data.ViewModels.Ingredients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.IngredientRepo
{
    public interface IIngredientRepo
    {
        Task<List<ViewIngredient>> GetActiveIngredients();
    }
}
