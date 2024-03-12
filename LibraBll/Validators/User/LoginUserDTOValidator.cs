using FluentValidation;
using LibraBll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Validators.Login
{
	public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
	{
		public LoginUserDTOValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Missing User Name");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Missing Password");
		}

	}
}
