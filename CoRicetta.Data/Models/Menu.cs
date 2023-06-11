using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public virtual User User { get; set; }
    }
}
