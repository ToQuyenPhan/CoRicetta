using CoRicetta.Data.Enum;
using CoRicetta.Data.ViewModels.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace CoRicetta.Data.ViewModels.Recipes
{
    public class RecipeFilterRequestModel : PagingRequestViewModel
    {
        [FromQuery(Name = "userId")]
        public int? UserId { get; set; }

        [FromQuery(Name = "recipeName")]
        public string RecipeName { get; set; }

        //[FromQuery(Name = "categoryId")]
        //public int? CategoryId { get; set; }

        [FromQuery(Name = "level")]
        public Level? Level { get; set; }
    }
}
