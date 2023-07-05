using CoRicetta.Data.ViewModels.Actions;
using CoRicetta.Data.ViewModels.Paging;
using System.Threading.Tasks;

namespace CoRicetta.Data.Repositories.ActionRepo
{
    public interface IActionRepo
    {
        Task<PagingResultViewModel<ViewAction>> GetActions(ActionRequestModel request);
        public void DeleteAction(int actionId);
        Task CreateAction(ActionFormModel model, int userId);
        Task<ViewAction> GetLike(ActionRequestModel model);
        Task DeleteActionsByRecipeId(int recipeId);
        Task UpdateComment(ActionFormModel model, int actionId);
        Task<ViewAction> GetActionById(int actionId);
    }
}
