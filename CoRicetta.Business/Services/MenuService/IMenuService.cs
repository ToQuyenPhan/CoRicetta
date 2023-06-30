using CoRicetta.Data.ViewModels.Menus;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.MenuService
{
    public interface IMenuService
    {
        Task CreateMenu(MenuFormViewModel model, string token);
    }
}
