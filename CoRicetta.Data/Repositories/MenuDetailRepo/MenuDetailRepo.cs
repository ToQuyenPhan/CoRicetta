﻿using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Menus;
using System.Collections.Generic;
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
            foreach(var recipe in model.Recipes)
            {
                var menuDetail = new MenuDetail
                {
                    MenuId = menuId,
                    RecipeId = recipe
                };
                menuDetails.Add(menuDetail);
            }
            await CreateRangeAsync(menuDetails);
        }
    }
}