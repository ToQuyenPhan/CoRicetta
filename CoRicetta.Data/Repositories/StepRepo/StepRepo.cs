using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Recipes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.StepRepo
{
    public class StepRepo : GenericRepo<Step>, IStepRepo
    {
        public StepRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task CreateSteps(RecipeFormViewModel model, int recipeId)
        {
            if(model.Steps.Any())
            {
                List<Step> steps = new List<Step>();
                for(int i = 0; i < model.Steps.Length; i++)
                {
                    var step = new Step
                    {
                        RecipeId = recipeId,
                        StepNumber = i + 1,
                        Description = model.Steps[i],
                        Status = 1
                    };
                    steps.Add(step);
                }
                await CreateRangeAsync(steps);
            }
        }

        public async Task UpdateSteps(RecipeFormViewModel model, int recipeId)
        {
            if (model.Steps.Any())
            {
                var query = from s in context.Steps where s.RecipeId.Equals(recipeId) select s;
                List<Step> items = await query.Select(selector => new Step
                {
                    Id = selector.Id,
                    RecipeId = selector.RecipeId,
                    StepNumber = selector.StepNumber,
                    Description = selector.Description,
                    Status = selector.Status
                }).ToListAsync();
                if (items.Count() > 0)
                {
                    await DeleteRangeAsync(items);
                }
                List<Step> steps = new List<Step>();
                for (int i = 0; i < model.Steps.Length; i++)
                {
                    var step = new Step
                    {
                        RecipeId = recipeId,
                        StepNumber = i + 1,
                        Description = model.Steps[i],
                        Status = 1
                    };
                    steps.Add(step);
                }
                await CreateRangeAsync(steps);
            }
        }
    }
}
