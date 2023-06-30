using CoRicetta.Data.Enum;
using System.Text.Json.Serialization;

namespace CoRicetta.Data.ViewModels.Menus
{
    public class ViewMenu
    {
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        public int Username { get; set; }

        [JsonPropertyName("menu_name")]
        public string MenuName { get; set; }
        public string Description { get; set; }
        public MenuStatus Status { get; set; }
    }
}
