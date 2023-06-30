using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoRicetta.Data.ViewModels.Menus
{
    public class ViewMenu
    {
        public int Id { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [JsonPropertyName("menu_name")]
        public string MenuName { get; set; }
        public string Description { get; set; }
        public MenuStatus Status { get; set; }

    }
}
