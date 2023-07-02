using CoRicetta.Data.ViewModels.Paging;
using Microsoft.AspNetCore.Mvc;

namespace CoRicetta.Data.ViewModels.Menus
{
    public class MenuFilterRequestModel : PagingRequestViewModel
    {
        [FromQuery(Name = "userId")]
        public int? UserId { get; set; }

        [FromQuery(Name = "menuName")]
        public string MenuName { get; set; }

        [FromQuery(Name = "menuStatus")]
        public int? MenuStatus { get; set; }
    }
}
