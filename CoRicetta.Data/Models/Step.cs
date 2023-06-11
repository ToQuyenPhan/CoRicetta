using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Step
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
