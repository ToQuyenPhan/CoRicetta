using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class RecipeDetail
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
