using System.Text.Json.Serialization;

namespace CoRicetta.Data.ViewModels.Ingredients
{
    public class ViewIngredient
    {
        public int Id { get; set; }

        [JsonPropertyName("ingredient_name")]
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public string Measurement { get; set; }
        public int Calories { get; set; }
    }
}
