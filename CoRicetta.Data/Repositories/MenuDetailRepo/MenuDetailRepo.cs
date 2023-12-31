﻿using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Menus;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.MenuDetailRepo
{
    public class MenuDetailRepo : GenericRepo<MenuDetail>, IMenuDetailRepo
    {
        public MenuDetailRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task CreateMenuDetail(MenuFormViewModel model, int menuId)
        {
            List<MenuDetail> menuDetails = new List<MenuDetail>();
            await CreateRangeAsync(menuDetails);
        }

        public async Task DeleteMenuDetailsByRecipeId(int recipeId)
        {
            var query = from md in context.MenuDetails where md.RecipeId.Equals(recipeId) select md;
            List<MenuDetail> items = await query.Select(selector => new MenuDetail
            {
                MenuId = selector.MenuId,
                RecipeId = selector.RecipeId
            }).ToListAsync();
            if (items.Count() > 0)
            {
                await DeleteRangeAsync(items);
            }
        }

        public async Task DeleteMenuDetailsByMenuId(int menuId)
        {
            var query = from md in context.MenuDetails where md.MenuId.Equals(menuId) select md;
            List<MenuDetail> items = await query.Select(selector => new MenuDetail
            {
                MenuId = selector.MenuId,
                RecipeId = selector.RecipeId
            }).ToListAsync();
            if (items.Count() > 0)
            {
                await DeleteRangeAsync(items);
            }
        }

        public async Task AddRecipe(int menuId, int recipeId)
        {
            bool check = false;
            var menuDetails = context.MenuDetails.Where(md => md.MenuId == menuId);
            foreach (var element in menuDetails)
                if (element.RecipeId == recipeId)
                {
                    check = true;
                }

            if (check == false)
            {
                var menu = new MenuDetail
                {
                    MenuId = menuId,
                    RecipeId = recipeId,
                };
                context.Add(menu);
                context.SaveChanges();
            }
        }
    }
}
