using CoRicetta.Data.Models;
using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Recipes;
using CoRicetta.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.MenuService
{
    public interface IMenuService
    {
        Task<PagingResultViewModel<ViewMenu>> getWithFilters(string token, MenuFilterRequestModel request);
        Task<PagingResultViewModel<ViewMenu>> getAllMenus(string token, MenuFilterRequestModel request);
    }
}
