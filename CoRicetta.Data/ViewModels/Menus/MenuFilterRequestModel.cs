using CoRicetta.Data.ViewModels.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoRicetta.Data.ViewModels.Menus
{
    public class MenuFilterRequestModel : PagingRequestViewModel
    {
        [FromQuery(Name = "userId")]
        public int? UserId { get; set; }

        [FromQuery(Name = "menuName")]
        public string MenuName { get; set; }
    }
}
