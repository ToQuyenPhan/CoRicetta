﻿using CoRicetta.Data.ViewModels.Actions;
using CoRicetta.Data.ViewModels.Paging;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.ActionService
{
    public interface IActionService
    {
        Task<PagingResultViewModel<ViewAction>> GetActions(string token, ActionRequestModel request);
        public void DeleteAction(string token, int actionId);
        Task CreateAction(ActionFormModel model, string token);
        Task<ViewAction> GetAction(string token, ActionRequestModel request);
        Task UpdateComment(ActionFormModel model, string token, int actionId);
    }
}
