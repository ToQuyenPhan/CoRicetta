using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class CategoryDetail
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
