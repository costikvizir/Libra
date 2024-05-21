using FluentValidation;
using LibraBll.Abstractions.Repositories;
using LibraBll.DTOs.User;
using System.Threading.Tasks;

namespace LibraBll.Validators.User
{
    public class AddUserDTOValidator : AbstractValidator<AddUserDTO>
    {
        private static string[] roles = { "Administrator", "User", "Technical Support" };
        private readonly IUserRepository _userRepository;

        public AddUserDTOValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(5).WithMessage("Name should contain at least 5 characters")
                .Matches("^[a-zA-Z ]*$").WithMessage("Username should contain only letters")
                .MustAsync((name, cancellation) => IsUniqueUserName(name)).WithMessage("Username already exists");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please provide a valid email")
                .Matches(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$").WithMessage("Not a valid email address")
                .MustAsync((email, cancellation) => IsUniqueEmail(email)).WithMessage("Email already exists");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Missing Login")
                .MustAsync((login, cancellation) => IsUniqueLogin(login)).WithMessage("Login already exists");

            RuleFor(x => x.Telephone)
               .NotEmpty().WithMessage("Missing Telephone")
               .MinimumLength(9).WithMessage("Not a phone number")
               .Matches(@"^\+?[0-9]*$").WithMessage("Phone number should contain only numbers")
               .MustAsync((telephone, cancellation) => IsUniqueTelephone(telephone)).WithMessage("Telephone already exists");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Missing Password")
                .MinimumLength(8).WithMessage("Password should contain at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            //RuleFor(x => x.Role).NotEmpty().WithMessage("Missing Role")
            //    .Must(role => roles.Contains(role.Trim().ToLower())).WithMessage("Invalid Role. Role must be either 'Administrator', 'User', or 'Technical Group'");
        }

        private async Task<bool> IsUniqueUserName(string name)
        {
            return !await _userRepository.UserNameExistsAsync(name);
        }

        private async Task<bool> IsUniqueEmail(string email)
        {
            return !await _userRepository.EmailExistsAsync(email);
        }

        private async Task<bool> IsUniqueLogin(string login)
        {
            return !await _userRepository.LoginExistsAsync(login);
        }

        private async Task<bool> IsUniqueTelephone(string telephone)
        {
            return !await _userRepository.TelephoneExistsAsync(telephone);
        }
    }
}