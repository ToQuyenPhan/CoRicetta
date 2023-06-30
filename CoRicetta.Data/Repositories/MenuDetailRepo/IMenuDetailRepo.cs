using CoRicetta.Data.ViewModels.Menus;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.MenuDetailRepo
{
    public interface IMenuDetailRepo
    {
        Task CreateMenuDetail(MenuFormViewModel model, int menuId);
    }
}
