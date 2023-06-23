using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<IList<Recipe>> getWithFilters(int? userId, string? name, int? categoryId, eLevel? level);
        Task<Recipe> getById(int recipeId);
    }
}
