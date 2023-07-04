using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.MenuRepo
{
    public interface IMenuRepo
    {
        Task CreateMenu(MenuFormViewModel model, int userId);
        Task<PagingResultViewModel<ViewMenu>> GetWithFilters(MenuFilterRequestModel request);
        Task<ViewMenu> GetMenuById(int menuId);
        Task UpdateMenu(MenuFormViewModel model, int userId);
        Task DeleteMenu(int menuId);
    }
}
