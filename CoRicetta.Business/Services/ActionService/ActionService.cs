using CoRicetta.Data.Repositories.ActionRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.ActionService
{
    public class ActionService : IActionService
    {
        private IActionRepo _actionRepo;

        public ActionService(IActionRepo actionRepo)
        {
            _actionRepo = actionRepo;
        }
    }
}
