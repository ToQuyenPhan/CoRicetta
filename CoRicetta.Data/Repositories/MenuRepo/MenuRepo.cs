using CoRicetta.Data.Context;
using CoRicetta.Data.Models;
using CoRicetta.Data.Repositories.GenericRepo;
using CoRicetta.Data.ViewModels.Menus;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.MenuRepo
{
    public class MenuRepo : GenericRepo<Menu>, IMenuRepo
    {
        public MenuRepo(CoRicettaDBContext context) : base(context)
        {
        }

        public async Task<int> CreateMenu(MenuFormViewModel model, int userId)
        {
            var menu = new Menu
            {
                UserId = userId,
                MenuName = model.MenuName,
                Description = model.Description,
                Status = (int)model.Status
            };
            await CreateAsync(menu);
            return menu.Id;
        }
    }
}
