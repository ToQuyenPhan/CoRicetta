using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CoRicetta.Data.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }
        [NotMapped]
        public ICollection<CategoryDetail> CategoryDetails { get; set; }
    }
}
