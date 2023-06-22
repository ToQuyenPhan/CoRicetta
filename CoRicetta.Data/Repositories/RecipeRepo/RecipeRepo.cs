using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Categories;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.RecipeRepo
{
    public class RecipeRepo : GenericRepo<Recipe>, IRecipeRepo
    {
        public RecipeRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<PagingResultViewModel<ViewRecipe>> GetRecipes(PagingRequestViewModel request)
        {
            var query = from r in context.Recipes join u in context.Users on r.UserId equals u.Id 
                        where r.Status.Equals((int) RecipeStatus.Public) select new { u, r };
            int totalCount = query.Count();
            List<ViewRecipe> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                          .Select(selector => new ViewRecipe()
                                          {
                                              Id = selector.r.Id,
                                              UserId = selector.r.UserId,
                                              UserName = selector.u.UserName,
                                              RecipeName = selector.r.RecipeName,
                                              Level = selector.r.Level,
                                              PrepareTime = selector.r.PrepareTime,
                                              CookTime = selector.r.CookTime,
                                              Image = selector.r.Image,
                                              Description = selector.r.Description,
                                              Status = ((RecipeStatus)selector.r.Status),
                                          }
                                          ).ToListAsync();
            foreach(var element in items)
            {
                element.Categories = await GetCategoriesInRecipe(element.Id);
            }
            return (items.Count() > 0) ? new PagingResultViewModel<ViewRecipe>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }

        public async Task<List<ViewCategory>> GetCategoriesInRecipe(int recipeId)
        {
            return await (from cd in context.CategoryDetails
                          join c in context.Categories on cd.CategoryId equals c.Id
                          where cd.RecipeId.Equals(recipeId)
                          select new ViewCategory
                          {
                              Id = c.Id,
                              CategoryName = c.CategoryName
                          }).ToListAsync();
        }
    }
}
