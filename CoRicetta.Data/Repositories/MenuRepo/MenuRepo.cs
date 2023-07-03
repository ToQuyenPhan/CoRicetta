using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CoRicetta.Data.Enum;
using Microsoft.EntityFrameworkCore;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.Models;
using CoRicetta.Data.Context;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Menus;

namespace CoRicetta.Data.Repositories.MenuRepo
{
    public class MenuRepo : GenericRepo<Menu>, IMenuRepo
    {
        public MenuRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task CreateMenu(MenuFormViewModel model, int userId)
        {
            var menu = new Menu
            {
                UserId = userId,
                MenuName = model.MenuName,
                Description = model.Description,
                Status = (int)model.Status
            };
            await CreateAsync(menu);
        }

        public async Task<PagingResultViewModel<ViewMenu>> GetWithFilters(MenuFilterRequestModel request)
        {

            var query = from m in context.Menus select m;
            if (request.MenuStatus.HasValue) query = query.Where(selector => selector.Status.Equals(request.MenuStatus));
            if (request.UserId.HasValue) query = query.Where(selector => selector.UserId.Equals(request.UserId));
            if (!string.IsNullOrEmpty(request.MenuName)) query = query.Where(selector => selector.MenuName.Contains(request.MenuName));
            int totalCount = query.Count();
            List<ViewMenu> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                          .Select(selector => new ViewMenu()
                                          {
                                              Id = selector.Id,
                                              UserId = selector.UserId,
                                              MenuName = selector.MenuName,
                                              Description = selector.Description,
                                              Status = (selector.Status.Equals(1)) ? "Public" : "Private"
                                          }
                                          ).ToListAsync();
            return (items.Count() > 0) ? new PagingResultViewModel<ViewMenu>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }
    }
}
