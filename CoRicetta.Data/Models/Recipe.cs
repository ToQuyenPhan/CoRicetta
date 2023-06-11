using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            Actions = new HashSet<Action>();
            Steps = new HashSet<Step>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecipeName { get; set; }
        public string Level { get; set; }
        public int PrepareTime { get; set; }
        public int CookTime { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
    }
}
