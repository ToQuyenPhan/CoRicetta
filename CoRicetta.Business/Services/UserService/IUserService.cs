using CoRicetta.Data.ViewModels.Paging;
using CoRicetta.Data.ViewModels.Users;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.UserService
{
    public interface IUserService
    {
        Task<string> Login(UserLoginViewModel model);
        Task<PagingResultViewModel<ViewUser>> GetUsers(string token, PagingRequestViewModel request);
        Task<string> SignUpAsync(UserRegisterViewModel model);
    }
}
