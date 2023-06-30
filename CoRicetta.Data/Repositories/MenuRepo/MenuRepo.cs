using CoRicetta.Data.Context;
using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Users;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.MenuRepo
{
    public class MenuRepo : GenericRepo<Menu>, IMenuRepo
    {
        private readonly IGenericRepo<Menu> _menuRepository;
        public MenuRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<PagingResultViewModel<ViewMenu>> getWithFilters(MenuFilterRequestModel request, int? userId)
        {

            var query = from m in context.Menus
                         where m.UserId == userId
                         select m;

            int totalCount = query.Count();

            List<ViewMenu> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                          .Select(selector => new ViewMenu()
                                          {
                                              Id = selector.Id,
                                              UserId = selector.UserId,
                                              MenuName = selector.MenuName,
                                              Description = selector.Description,
                                              Status = (MenuStatus)selector.Status
                                          }
                                          ).ToListAsync();
            return (items.Count() > 0) ? new PagingResultViewModel<ViewMenu>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }
        public async Task<PagingResultViewModel<ViewMenu>> getAllMenus(MenuFilterRequestModel request)
        {

            var query = from m in context.Menus
                        where m.Status.Equals((int) MenuStatus.Public)
                        select m;

            int totalCount = query.Count();

            List<ViewMenu> items = await query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                                          .Select(selector => new ViewMenu()
                                          {
                                              Id = selector.Id,
                                              UserId = selector.UserId,
                                              MenuName = selector.MenuName,
                                              Description = selector.Description,
                                              Status = (MenuStatus)selector.Status
                                          }
                                          ).ToListAsync();
            return (items.Count() > 0) ? new PagingResultViewModel<ViewMenu>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }
    }
}
