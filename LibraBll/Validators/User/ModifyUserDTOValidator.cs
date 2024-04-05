using FluentValidation;
using LibraBll.DTOs;
using LibraBll.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Validators.User
{
	public class ModifyUserDTOValidator : AbstractValidator<ModifyUserDTO>
	{
		private static string[] roles = { "admin", "user", "technical group" };
		public ModifyUserDTOValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
				.MinimumLength(5).WithMessage("Name should contain at least 5 characters");

			RuleFor(x => x.Email).NotEmpty().WithMessage("Please provide a valid email")
				.EmailAddress().WithMessage("Not an email address");

			RuleFor(x => x.Login).NotEmpty().WithMessage("Missing Login");

			RuleFor(x => x.Telephone).NotEmpty().WithMessage("Missing Telephone");

			RuleFor(x => x.Role).NotEmpty().WithMessage("Missing Role")
				.Must(role => roles.Contains(role.Trim().ToLower())).WithMessage("Invalid Role. Role must be either 'admin', 'user', or 'technical group'");
		}
	}
}
