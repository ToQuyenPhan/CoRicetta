﻿using CoRicetta.Data.Enum;
using System.Text.Json.Serialization;

namespace CoRicetta.Data.ViewModels.Users
{
    public class ViewUser
    {
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; }
    }
}
