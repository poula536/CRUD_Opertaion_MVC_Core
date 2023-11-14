﻿using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class forgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

	}
}
