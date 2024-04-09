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
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(5).WithMessage("Name should contain at least 5 characters")
                .Matches("^[a-zA-Z]*$").WithMessage("Username shoul contain only letters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please provide a valid email")
                .Matches(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$").WithMessage("Not a valid email address");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Missing Login");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Missing Telephone")
                .MinimumLength(9).WithMessage("Not a phone number")
                .Matches(@"^\+?[0-9]*$").WithMessage("Phone number should contain only numbers");

            //RuleFor(x => x.Password)
            //    .NotEmpty().WithMessage("Missing Password")
            //    .MinimumLength(8).WithMessage("Password should contain at least 8 characters")
            //    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            //    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            //    .Matches("[0-9]").WithMessage("Password must contain at least one number")
            //    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
        }
    }
}