using CoRicetta.Data.Enum;

namespace CoRicetta.Data.ViewModels.Users
{
    public class UserFormViewModel
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; }
    }
}
