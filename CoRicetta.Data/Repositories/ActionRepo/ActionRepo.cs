using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Actions;
using CoRicetta.Data.ViewModels.Paging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.ActionRepo
{
    public class ActionRepo : GenericRepo<Action>, IActionRepo
    {
        public ActionRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<PagingResultViewModel<ViewAction>> GetActions(ActionRequestModel request)
        {
            var query = from a in context.Actions
                        join u in context.Users on a.UserId equals u.Id
                        join r in context.Recipes on a.RecipeId equals r.Id
                        select new { u, r, a };        
            if (request.UserId.HasValue) query = query.Where(selector => selector.a.UserId.Equals(request.UserId));
            if (request.RecipeId.HasValue) query = query.Where(selector => selector.a.RecipeId.Equals(request.RecipeId));
            if (request.Type.HasValue)
                query = query.Where(selector => selector.a.Type.Equals((int)request.Type));
            int totalCount = query.Count();
            List<ViewAction> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                          .Select(selector => new ViewAction()
                                          {
                                              Id = selector.a.Id,
                                              UserId = selector.a.UserId,
                                              RecipeId = selector.a.RecipeId,
                                              Content = selector.a.Content,
                                              Type = ((ActionType)selector.a.Type),
                                              DateTime = selector.a.DateTime,
                                              Status = selector.a.Status,
                                              Username = selector.u.UserName
                                          }
                                          ).OrderByDescending(a => a.DateTime).ToListAsync();

            return (items.Count() > 0) ? new PagingResultViewModel<ViewAction>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }

        public void DeleteAction(int actionId)
        {
            Action action = context.Actions.FindAsync(actionId).Result;
            if (action != null)
            {
                context.Actions.Remove(action);
                context.SaveChanges();
            }
        }

        public async Task DeleteActionsByRecipeId(int recipeId)
        {
            var query = from a in context.Actions where a.RecipeId.Equals(recipeId) select a;
            List<Action> items = await query.Select(selector => new Action
            {
                Id = selector.Id,
                UserId = selector.UserId,
                RecipeId = selector.RecipeId,
                Type = selector.Type,
                Content = selector.Content,
                DateTime = selector.DateTime,
                Status = selector.Status
            }).ToListAsync();
            if (items.Count() > 0)
            {
                await DeleteRangeAsync(items);
            }
        }

        public async Task CreateAction(ActionFormModel model, int userId)
        {
            var action = new Action
            {
                UserId = userId,
                RecipeId = model.RecipeId,
                Type = model.Type,
                Content = model.Content,
                DateTime = System.DateTime.Now.ToLocalTime(),
                Status = 1
            };
            await CreateAsync(action);
        }

        public async Task<ViewAction> GetLike(ActionRequestModel model)
        {
            var query = from a in context.Actions
                        join u in context.Users on a.UserId equals u.Id
                        join r in context.Recipes on a.RecipeId equals r.Id
                        where a.UserId.Equals(model.UserId) && a.RecipeId.Equals(model.RecipeId) 
                        && a.Type.Equals((int)model.Type)
                        select new { u, r, a };
            return await query.Select(selector => new ViewAction()
            {
                Id = selector.a.Id,
                UserId = selector.a.UserId,
                RecipeId = selector.a.RecipeId,
                Content = selector.a.Content,
                Type = ((ActionType)selector.a.Type),
                DateTime = selector.a.DateTime,
                Status = selector.a.Status,
                Username = selector.u.UserName
            }).FirstOrDefaultAsync();
        }
    }
}
