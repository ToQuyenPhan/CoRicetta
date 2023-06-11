using CoRicetta.Data.Repositories.RecipeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private IRecipeRepo _recipeRepo;

        public RecipeService(IRecipeRepo recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }
    }
}
