﻿using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.MenuDetailRepo;
using CoRicetta.Data.Repositories.MenuRepo;
using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
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
            _decodeToken = new DecodeToken();
        }

        public async Task<PagingResultViewModel<ViewMenu>> getWithFilters(string token, MenuFilterRequestModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            int id = Int32.Parse(_decodeToken.DecodeText(token, "Id"));
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }

            PagingResultViewModel<ViewMenu> menus = await _menuRepo.getWithFilters(request, id);
            if (menus.Items == null) throw new NullReferenceException("Not found any menus");
            return menus;
        }
        public async Task<PagingResultViewModel<ViewMenu>> getAllMenus(string token, MenuFilterRequestModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            int id = Int32.Parse(_decodeToken.DecodeText(token, "Id"));
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }

            PagingResultViewModel<ViewMenu> menus = await _menuRepo.getAllMenus(request);
            if (menus.Items == null) throw new NullReferenceException("Not found any menus");
            return menus;
            //_menuDetailRepo = menuDetailRepo;
            //_decodeToken = new DecodeToken();
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

        public async Task<PagingResultViewModel<ViewMenu>> GetWithFilters(string token, MenuFilterRequestModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            PagingResultViewModel<ViewMenu> menus = await _menuRepo.GetWithFilters(request);
            if (menus.Items == null) throw new NullReferenceException("Not found any menus");
            return menus;
        }
    }
}
