using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Report
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
