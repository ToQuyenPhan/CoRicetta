﻿using CoRicetta.Data.ViewModels.Ingredients;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<PagingResultViewModel<ViewRecipe>> GetRecipes(string token, RecipeFilterRequestModel request);
        Task<ViewRecipe> getById(int recipeId);
        Task CreateRecipe(RecipeFormViewModel model, string token);
        Task UpdateRecipe(RecipeFormViewModel model, string token, int recipeId);
        Task DeleteRecipe(string token, int recipeId);
        Task<List<ViewIngredient>> GetShoppingListWithId(int recipeId);
        Task<PagingResultViewModel<ViewRecipe>> GetSharedRecipes(string token, RecipeFilterRequestModel request);
    }
}
