using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using System.Collections.Generic;
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
        Task<PagingResultViewModel<ViewMenu>> GetWithUserId(MenuFilterRequestModel request);

        Task<bool> canAddRecipe(int menuId, int recipeId);
        Task<List<ViewMenu>> GetWithUserIdExceptRecipeAdded(int userId, int recipeId);
    }
}
