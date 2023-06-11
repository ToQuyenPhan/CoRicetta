using System;
using System.Collections.Generic;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }
    }
}
