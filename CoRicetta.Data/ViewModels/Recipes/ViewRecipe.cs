using CoRicetta.Data.Enum;
using CoRicetta.Data.ViewModels.Categories;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoRicetta.Data.ViewModels.Recipes
{
    public class ViewRecipe
    {
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("recipe_name")]
        public string RecipeName { get; set; }
        public string Level { get; set; }

        [JsonPropertyName("prepare_time")]
        public int PrepareTime { get; set; }

        [JsonPropertyName("cook_time")]
        public int CookTime { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public RecipeStatus Status { get; set; }

        public List<ViewCategory> Categories { get; set; }
    }
}
