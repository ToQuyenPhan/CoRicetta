using CoRicetta.Data.ViewModels.Menus;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.MenuRepo
{
    public interface IMenuRepo
    {
        Task<int> CreateMenu(MenuFormViewModel model, int userId);
    }
}
