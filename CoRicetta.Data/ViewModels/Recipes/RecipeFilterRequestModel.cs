﻿using CoRicetta.Data.Enum;
using CoRicetta.Data.ViewModels.Paging;
using Microsoft.AspNetCore.Mvc;

namespace CoRicetta.Data.ViewModels.Recipes
{
    public class RecipeFilterRequestModel : PagingRequestViewModel
    {
        [FromQuery(Name = "userId")]
        public int? UserId { get; set; }

        [FromQuery(Name = "recipeName")]
        public string RecipeName { get; set; }

        [FromQuery(Name = "categoryId")]
        public int? CategoryId { get; set; }

        [FromQuery(Name = "level")]
        public Level? Level { get; set; }

        [FromQuery(Name = "recipeStatus")]
        public int? RecipeStatus { get; set; }
    }
}
