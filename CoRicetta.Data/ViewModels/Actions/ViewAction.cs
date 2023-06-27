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
        [JsonPropertyName("user_it")]
        public int UserId { get; set; }
        [JsonPropertyName("recipe_it")]
        public int RecipeId { get; set; }
        [JsonPropertyName("type")]
        public ActionType Type { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }
    }
}
