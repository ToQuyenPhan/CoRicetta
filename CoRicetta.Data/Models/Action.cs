using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Action
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
