using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.UserRepo
{
    public interface IUserRepo
    {
        Task<UserTokenViewModel> GetByEmailAndPassword(string email, string password);
        Task<bool> CheckEmailAndPassword(string email, string password);
        Task<PagingResultViewModel<ViewUser>> GetUsers(PagingRequestViewModel request);
    }
}
