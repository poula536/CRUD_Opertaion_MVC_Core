using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public IEnumerable<string> Roles { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Confirm Password does not match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAgree { get; set; }

    }
}
