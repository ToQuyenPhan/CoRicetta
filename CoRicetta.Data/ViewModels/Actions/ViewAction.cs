using CoRicetta.Data.Enum;
using CoRicetta.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoRicetta.Data.ViewModels.Actions
{
    public class ViewAction
    {
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("recipe_id")]
        public int RecipeId { get; set; }
        public ActionType Type { get; set; }
        public string Content { get; set; }

        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }
        public int Status { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
