using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string Measurement { get; set; }
        public int Calories { get; set; }
        public int Status { get; set; }
    }
}
