using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email or UserName is required")]
		//[EmailAddress(ErrorMessage = "Invalid Email or UserName")]
		[Display(Name ="Email Or Username")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }

	}
}
