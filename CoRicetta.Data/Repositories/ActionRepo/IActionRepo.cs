using CoRicetta.Data.ViewModels.Actions;
using CoRicetta.Data.ViewModels.Paging;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.ActionRepo
{
    public interface IActionRepo
    {
        Task<PagingResultViewModel<ViewAction>> GetActions(ActionRequestModel request);
        public void DeleteAction(int actionId);
        Task CreateComment(ActionFormModel model, int userId);
    }
}
