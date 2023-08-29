using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Validations
{
    public class RegisterOrganizerValidator : AbstractValidator<RegisterOrganizerModel>
    {
        public RegisterOrganizerValidator ()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MaximumLength(50).WithMessage("Full name must be less than 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .MaximumLength(200).WithMessage("Email must be less than 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required for accepting your request");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                //.MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(200).WithMessage("Password cannot be longer than 200 characters")
                .Must((model, password) => model.PasswordVerification == password).WithMessage("Passwords don't match!");
        }
    }
}
