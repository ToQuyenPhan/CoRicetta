using System.Text.Json.Serialization;

namespace CoRicetta.Data.ViewModels.Categories
{
    public class ViewCategory
    {
        public int Id { get; set; }

        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; }
        public int Status { get; set; }
    }
}
