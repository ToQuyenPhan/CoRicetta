using CoRicetta.Data.Models;
using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.MenuRepo
{
    public interface IMenuRepo
    {
        Task<PagingResultViewModel<ViewMenu>> getWithFilters(MenuFilterRequestModel request, int? userId);
        Task<PagingResultViewModel<ViewMenu>> getAllMenus(MenuFilterRequestModel request);

    }
}
