using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class MenuDetail
    {
        public int MenuId { get; set; }
        public int RecipeId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
