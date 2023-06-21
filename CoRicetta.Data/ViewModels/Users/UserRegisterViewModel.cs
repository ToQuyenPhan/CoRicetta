using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoRicetta.Data.ViewModels.Users
{
    public class UserRegisterViewModel
    {
        [EmailAddress]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Email minimum length is 6 and maximum length is 50.")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password minimum length is 6 and maximum length is 50.")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Full name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name minimum length is 5 and maximum length is 50.")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$", ErrorMessage = "The phone number is invalid.")]
        public string Phone { get; set; } = string.Empty;
    }
}
