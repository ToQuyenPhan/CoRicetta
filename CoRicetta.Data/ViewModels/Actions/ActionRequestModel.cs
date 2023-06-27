using CoRicetta.Data.Enum;
using CoRicetta.Data.ViewModels.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.ViewModels.Actions
{
    public class ActionRequestModel : PagingRequestViewModel
    {
        [FromQuery(Name = "actionId")]
        public int? ActionId { get; set; }

        [FromQuery(Name = "userId")]
        public int? UserId { get; set; }

        [FromQuery(Name = "recipeId")]
        public int? RecipeId { get; set; }

        [FromQuery(Name = "Type")]
        public ActionType? Type { get; set; }


    }
}
