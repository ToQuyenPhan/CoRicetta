﻿using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.MenuDetailRepo;
using CoRicetta.Data.Repositories.MenuRepo;
using CoRicetta.Data.ViewModels.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.MenuService
{
    public class MenuService : IMenuService
    {
        private IMenuRepo _menuRepo;
        private IMenuDetailRepo _menuDetailRepo;
        private DecodeToken _decodeToken;

        public MenuService(IMenuRepo menuRepo, IMenuDetailRepo menuDetailRepo)
        {
            _menuRepo = menuRepo;
            _menuDetailRepo = menuDetailRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task CreateMenu(MenuFormViewModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            int userId = _decodeToken.Decode(token, "Id");
            if (model.Recipes.Any())
            {
                int menuId = await _menuRepo.CreateMenu(model, userId);
                await _menuDetailRepo.CreateMenuDetail(model, menuId);
            }
            else
            {
                throw new ArgumentException("You need to add a recipe at least!");
            }
        }
    }
}
