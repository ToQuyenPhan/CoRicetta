using CoRicetta.Business.Utils;
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
        private DecodeToken _decodeToken;

        public IngredientService(IIngredientRepo ingredientRepo)
        {
            _ingredientRepo = ingredientRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task<List<ViewIngredient>> GetActiveIngredients()
        {
            List<ViewIngredient> items = await _ingredientRepo.GetActiveIngredients();
            if (items == null) throw new NullReferenceException("Not found any ingredients");
            return items;
        }
        
        public async Task UpdateIngredient(IngredientFormModel model, string token, int ingredientId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            var ingredient = await _ingredientRepo.GetIngredientById(ingredientId);
            if (ingredient == null) throw new NullReferenceException("Not found any ingredients!");
            await _ingredientRepo.UpdateIngredient(model, ingredientId);
        }

        public async Task<ViewIngredient> GetIngredientById(string token, int ingredientId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to view this resource!");
            }
            var ingredient = await _ingredientRepo.GetIngredientById(ingredientId);
            if (ingredient == null) throw new NullReferenceException("Not found any ingredients!");
            return ingredient;
        }

        public async Task CreateIngredient(IngredientFormModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            var isExisted = await _ingredientRepo.IsExistedIngredient(model);
            if (isExisted) throw new ArgumentException("Nguyên liệu đã tồn tại!");
            await _ingredientRepo.CreateIngredient(model);
        }

        public async Task DeleteIngredient(int ingredientId, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("USER"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            var ingredient = await _ingredientRepo.GetIngredientById(ingredientId);
            if (ingredient == null) throw new NullReferenceException("Not found any ingredients!");
            await _ingredientRepo.DeleteIngredient(ingredientId);
        }
    }
}
