using CoRicetta.Data.Repositories.MenuRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.MenuService
{
    public class MenuService : IMenuService
    {
        private IMenuRepo _menuRepo;

        public MenuService(IMenuRepo menuRepo)
        {
            _menuRepo = menuRepo;
        }
    }
}
