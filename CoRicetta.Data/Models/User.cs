using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class User
    {
        public User()
        {
            Actions = new HashSet<Action>();
            Menus = new HashSet<Menu>();
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
