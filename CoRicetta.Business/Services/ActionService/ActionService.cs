using CoRicetta.Business.Utils;
using CoRicetta.Data.Repositories.ActionRepo;
using CoRicetta.Data.ViewModels.Actions;
using CoRicetta.Data.ViewModels.Paging;
using System;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.ActionService
{
    public class ActionService : IActionService
    {
        private IActionRepo _actionRepo;
        private DecodeToken _decodeToken;

        public ActionService(IActionRepo actionRepo)
        {
            _actionRepo = actionRepo;
            _decodeToken = new DecodeToken();
        }

        public async Task<PagingResultViewModel<ViewAction>> GetActions(string token, ActionRequestModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            PagingResultViewModel<ViewAction> actions = await _actionRepo.GetActions(request);
            if (actions.Items == null) throw new NullReferenceException("Not found any action");
            return actions;
        }

        public void DeleteAction(string token, int actionId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            _actionRepo.DeleteAction(actionId);
        }

        public async Task CreateAction(ActionFormModel model, string token)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            int userId = _decodeToken.Decode(token, "Id");
            await _actionRepo.CreateAction(model, userId);
        }

        public async Task<ViewAction> GetLike(string token, ActionRequestModel request)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to access this resource!");
            }
            ViewAction action = await _actionRepo.GetLike(request);
            if (action == null) throw new NullReferenceException("Not found any action");
            return action;
        }

        public async Task UpdateComment(ActionFormModel model, string token, int actionId)
        {
            string role = _decodeToken.DecodeText(token, "Role");
            if (role.Equals("ADMIN"))
            {
                throw new UnauthorizedAccessException("You do not have permission to do this action!");
            }
            var action = await _actionRepo.GetActionById(actionId);
            if (action == null) throw new NullReferenceException("Not found any actions!");
            await _actionRepo.UpdateComment(model, actionId);
        }
    }
}
