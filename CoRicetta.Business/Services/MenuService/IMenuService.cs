using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.MenuService
{
    public interface IMenuService
    {
        Task CreateMenu(MenuFormViewModel model, string token);
        Task<PagingResultViewModel<ViewMenu>> GetWithFilters(string token, MenuFilterRequestModel request);
        Task<ViewMenu> GetMenuById(string token, int menuId);
    }
}
