using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Ingredients;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.IngredientRepo
{
    public class IngredientRepo : GenericRepo<Ingredient>, IIngredientRepo
    {
        public IngredientRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<List<ViewIngredient>> GetActiveIngredients()
        {
            var query = from i in context.Ingredients where i.Status.Equals((int)IngredientStatus.Active) select i;
            List<ViewIngredient> items = await query.Select(i => new ViewIngredient
            {
                Id = i.Id,
                IngredientName = i.IngredientName,
                Quantity = 0,
                Measurement = i.Measurement,
                Calories = i.Calories,
                Status = i.Status,
                StatusString = (i.Status.Equals(1)) ? "Đang hiển thị" : "Không hiển thị"
            }).ToListAsync();
            return (items.Count() > 0) ? items : null;
        }

        public async Task UpdateIngredient(IngredientFormModel model, int ingredientId)
        {
            var query = from i in context.Ingredients where i.Id.Equals(ingredientId) select i;
            var ingredient = await query.Select(selector => new Ingredient
            {
                Id = selector.Id,
                IngredientName = selector.IngredientName,
                Measurement = selector.Measurement,
                Calories = selector.Calories,
                Status = selector.Status,
            }).FirstOrDefaultAsync();
            ingredient.IngredientName = model.IngredientName;
            ingredient.Measurement = model.Measurement;
            ingredient.Calories = model.Calories;
            ingredient.Status = (int)model.Status;
            await UpdateAsync(ingredient);
        }

        public async Task<ViewIngredient> GetIngredientById(int ingredientId)
        {
            var query = from i in context.Ingredients where i.Id.Equals(ingredientId) select i;
            return await query.Select(selector => new ViewIngredient
            {
                Id = selector.Id,
                IngredientName = selector.IngredientName,
                Quantity = 0,
                Measurement = selector.Measurement,
                Calories = selector.Calories,
                Status = selector.Status,
                StatusString = (selector.Status.Equals(1)) ? "Đang hiển thị" : "Không hiển thị"
            }).FirstOrDefaultAsync();
        }

        public async Task CreateIngredient(IngredientFormModel model)
        {
            var ingredient = new Ingredient
            {
                IngredientName = model.IngredientName,
                Measurement = model.Measurement,
                Calories = model.Calories,
                Status = (int)model.Status
            };
            await CreateAsync(ingredient);
        }

        public async Task<bool> IsExistedIngredient(IngredientFormModel model)
        {
            var query = from i in context.Ingredients 
                        where i.IngredientName.Equals(model.IngredientName.Trim()) && i.Measurement.Equals(model.Measurement.Trim())
                        && i.Status.Equals((int) IngredientStatus.Active)
                        select i;
            var ingredient = await query.Select(selector => new ViewIngredient
            {
                Id = selector.Id,
                IngredientName = selector.IngredientName,
                Quantity = 0,
                Measurement = selector.Measurement,
                Calories = selector.Calories,
                Status = selector.Status,
                StatusString = (selector.Status.Equals(1)) ? "Đang hiển thị" : "Không hiển thị"
            }).FirstOrDefaultAsync();
            return (ingredient != null) ? true : false;
        }

        public async Task DeleteIngredient(int ingredientId)
        {
            var query = from i in context.Ingredients where i.Id.Equals(ingredientId) select i;
            var ingredient = await query.Select(selector => new Ingredient
            {
                Id = selector.Id,
                IngredientName = selector.IngredientName,
                Measurement = selector.Measurement,
                Calories = selector.Calories,
                Status = selector.Status,
            }).FirstOrDefaultAsync();
            await DeleteAsync(ingredient);
        }

        public async Task<List<ViewIngredient>> GetInactiveIngredients()
        {
            var query = from i in context.Ingredients where i.Status.Equals((int)IngredientStatus.Inactive) select i;
            List<ViewIngredient> items = await query.Select(i => new ViewIngredient
            {
                Id = i.Id,
                IngredientName = i.IngredientName,
                Quantity = 0,
                Measurement = i.Measurement,
                Calories = i.Calories,
                Status = i.Status,
                StatusString = (i.Status.Equals(1)) ? "Đang hiển thị" : "Không hiển thị"
            }).ToListAsync();
            return (items.Count() > 0) ? items : null;
        }
    }
}
