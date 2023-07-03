using System.Text.Json.Serialization;

namespace CoRicetta.Data.ViewModels.Reports
{
    public class ViewReport
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("recipe_id")]
        public int RecipeId { get; set; }

        [JsonPropertyName("recipe_name")]
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
