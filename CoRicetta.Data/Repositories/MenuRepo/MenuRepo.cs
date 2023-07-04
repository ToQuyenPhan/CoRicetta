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
using CoRicetta.Data.ViewModels.Recipes;

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

        public async Task UpdateMenu(MenuFormViewModel model, int userId)
        {
            var menu = new Menu
            {
                Id = (int)model.MenuId,
                UserId = userId,
                MenuName = model.MenuName,
                Description = model.Description,
                Status = (int)model.Status
            };
            await UpdateAsync(menu);
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
            foreach (var item in items)
            {
                item.Recipes = await GetRecipesInMenu(item.Id);
            }
            return (items.Count() > 0) ? new PagingResultViewModel<ViewMenu>(items, totalCount, request.CurrentPage, request.PageSize) : null;
        }

        public async Task<ViewMenu> GetMenuById(int menuId)
        {
            var query = from m in context.Menus where m.Id.Equals(menuId) select m;
            ViewMenu item = await query.Select(selector => new ViewMenu()
            {
                Id = selector.Id,
                UserId = selector.UserId,
                MenuName = selector.MenuName,
                Description = selector.Description,
                Status = (selector.Status.Equals(1)) ? "Public" : "Private"
            }).FirstOrDefaultAsync();
            item.Recipes = await GetRecipesInMenu(item.Id);
            return (item != null) ? item : null;
        }

        public async Task DeleteMenu(int menuId)
        {
            var menu = await GetMenu(menuId);
            await DeleteAsync(menu);
        }

        private async Task<List<ViewRecipe>> GetRecipesInMenu(int menuId)
        {
            return await (from md in context.MenuDetails
                          join r in context.Recipes on md.RecipeId equals r.Id
                          join u in context.Users on r.UserId equals u.Id
                          where md.MenuId.Equals(menuId)
                          select new ViewRecipe
                          {
                              Id = r.Id,
                              UserId = r.UserId,
                              UserName = u.UserName,
                              RecipeName = r.RecipeName,
                              Level = r.Level,
                              PrepareTime = r.PrepareTime,
                              CookTime = r.CookTime,
                              Image = r.Image,
                              Description = r.Description,
                              Status = ((RecipeStatus)r.Status).ToString(),
                          }).ToListAsync();
        }

        private async Task<Menu> GetMenu(int menuId)
        {
            var query = from m in context.Menus where m.Id.Equals(menuId) select m;
            return query.FirstOrDefault();
        }
    }
}
