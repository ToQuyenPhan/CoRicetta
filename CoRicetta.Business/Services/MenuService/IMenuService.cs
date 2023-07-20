using CoRicetta.Data.ViewModels.Menus;
using CoRicetta.Data.ViewModels.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.MenuService
{
    public interface IMenuService
    {
        Task CreateMenu(MenuFormViewModel model, string token);
        Task<PagingResultViewModel<ViewMenu>> GetWithFilters(string token, MenuFilterRequestModel request);
        Task<List<ViewMenu>> GetWithUserIdExceptRecipeAdded(string token,int userId, int recipeId);
        Task<ViewMenu> GetMenuById(string token, int menuId);
        Task UpdateMenu(MenuFormViewModel model, string token);
        Task DeleteMenu(int menuId, string token);
        Task AddRecipe(int menuId, int recipeId, string token);
        Task<bool> canAddRecipe(int menuId, int recipeId, string token);
        Task<PagingResultViewModel<ViewMenuByUserId>> GetWithUserId(string token, MenuFilterRequestModel request);


    }
}
