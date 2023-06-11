using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoRicetta.Data.ViewModels.Paging
{
    public class PagingRequestViewModel
    {
        const int MAX_PAGE_SIZE = 50;
        private int _pageSize = 10;

        // change name of parameter
        [FromQuery(Name = "currentPage")]
        public int CurrentPage { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
            }
        }
    }
}
