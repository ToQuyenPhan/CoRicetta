using CoRicetta.Data.Enum;
using System.Text.Json.Serialization;
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
