using CoRicetta.Data.Repositories.IngredientRepo;
using CoRicetta.Data.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.IngredientService
{
    public class IngredientService : IIngredientService
    {
        private IIngredientRepo _ingredientRepo;

        public IngredientService(IIngredientRepo ingredientRepo)
        {
            _ingredientRepo = ingredientRepo;
        }

        public async Task<List<ViewIngredient>> GetActiveIngredients()
        {
            List<ViewIngredient> items = await _ingredientRepo.GetActiveIngredients();
            if (items == null) throw new NullReferenceException("Not found any ingredients");
            return items;
        }

    }
}
