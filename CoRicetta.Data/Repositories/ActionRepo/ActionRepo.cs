using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Actions;
using CoRicetta.Data.ViewModels.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                        //where r.Status.Equals((int)RecipeStatus.Public)
                        select new { u, r, a };
            int totalCount = query.Count();
            if (request.ActionId.HasValue) query = query.Where(selector => selector.a.Id.Equals(request.ActionId));
            if (request.UserId.HasValue) query = query.Where(selector => selector.a.UserId.Equals(request.UserId));
            if (request.RecipeId.HasValue) query = query.Where(selector => selector.a.RecipeId.Equals(request.RecipeId));
            if (request.Type.HasValue)
                switch (request.Type.ToString())
                {
                    case "Comment":
                        query = query.Where(selector => selector.a.Type.Equals((int)request.Type));
                        break;
                    case "Like":
                        query = query.Where(selector => selector.a.Type.Equals((int)request.Type));
                        break;
                    case "Save":
                        query = query.Where(selector => selector.a.Type.Equals((int)request.Type));
                        break;
                    default:
                        break;

                }

            //if (request.Level.HasValue) query = query.Where(selector => selector.r.Level.Equals(request.Level.ToString()));
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
                                          }
                                          ).ToListAsync();

            return (items.Count() > 0) ? new PagingResultViewModel<ViewAction>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }
    }
}
