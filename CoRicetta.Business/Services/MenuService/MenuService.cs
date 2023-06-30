using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.MenuRepo;
using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
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
        private DecodeToken _decodeToken;

        public MenuService(IMenuRepo menuRepo)
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
        }
    }
}
