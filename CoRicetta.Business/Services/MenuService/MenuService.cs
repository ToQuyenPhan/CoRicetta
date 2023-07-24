using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.MenuDetailRepo;
using CoRicetta.Data.Repositories.MenuRepo;
using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using System;
using System.Collections.Generic;
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
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            int userId = _decodeToken.Decode(token, "Id");
            await _menuRepo.CreateMenu(model, userId);
        }

        public async Task UpdateMenu(MenuFormViewModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            int userId = _decodeToken.Decode(token, "Id");
            await _menuRepo.UpdateMenu(model, userId);
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

        public async Task<ViewMenu> GetMenuById(string token, int menuId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            ViewMenu menu = await _menuRepo.GetMenuById(menuId);
            if (menu == null) throw new NullReferenceException("Menu not found");
            foreach (var recipe in menu.Recipes)
            {
                switch (recipe.Level.ToString())
                {
                    case "Easy":
                        recipe.Level = "Dễ";
                        break;
                    case "Normal":
                        recipe.Level = "Trung bình";
                        break;
                    case "Hard":
                        recipe.Level = "Khó";
                        break;
                    default:
                        break;
                }
            }
            return menu;
        }

        public async Task DeleteMenu(int menuId, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            var menu = await _menuRepo.GetMenuById(menuId);
            if (menu == null) throw new NullReferenceException("Not found any menus!");
            await _menuDetailRepo.DeleteMenuDetailsByMenuId(menuId);
            await _menuRepo.DeleteMenu(menu.Id);
        }

        public async Task AddRecipe(int menuId, int recipeId, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            await _menuDetailRepo.AddRecipe(menuId, recipeId);
        }

        public async Task<List<ViewMenu>> GetWithUserIdExceptRecipeAdded(string token, int userId, int recipeId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            List<ViewMenu> menus = await _menuRepo.GetWithUserIdExceptRecipeAdded(userId, recipeId);
            return menus;
        }

        public async Task<bool> canAddRecipe(int menuId, int recipeId, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            return await _menuRepo.canAddRecipe(menuId, recipeId);
        }

        public async Task<PagingResultViewModel<ViewMenu>> GetWithUserId(string token, MenuFilterRequestModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            PagingResultViewModel<ViewMenu> menus = await _menuRepo.GetWithUserId(request);
            if (menus.Items == null) throw new NullReferenceException("Not found any menus");
            return menus;
        }
    }
}
