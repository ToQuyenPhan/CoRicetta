﻿using CoRicetta.Data.ViewModels.Actions;
using CoRicetta.Data.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Business.Services.ActionService
{
    public interface IActionService
    {
        Task<PagingResultViewModel<ViewAction>> GetActions(string token, ActionRequestModel request);
    }
}
