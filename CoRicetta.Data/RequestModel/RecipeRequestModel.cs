using CoRicetta.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoRicetta.Data.RequestModel
{
    public class RecipeRequestModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public eLevel Level { get; set; }

    }
}
