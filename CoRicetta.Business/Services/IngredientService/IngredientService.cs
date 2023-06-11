using CoRicetta.Data.Repositories.IngredientRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
