using System.ComponentModel.DataAnnotations;

namespace CoRicetta.Data.ViewModels.Users
{
    public class UserRegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Username { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$", ErrorMessage = "The phone number is invalid.")]
        public string PhoneNumber { get; set; }
    }
}
