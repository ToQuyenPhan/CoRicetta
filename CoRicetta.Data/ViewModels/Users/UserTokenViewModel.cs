using CoRicetta.Data.Enum;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoRicetta.Data.ViewModels.Users
{
    public class UserTokenViewModel
    {
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; }
    }
}
