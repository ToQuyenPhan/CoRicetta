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
            }).ToListAsync();
            return items;
        }
    }
}
