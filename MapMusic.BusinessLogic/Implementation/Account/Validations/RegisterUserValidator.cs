using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Validations
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .MaximumLength(200).WithMessage("Email cannot be longer than 200 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                //.MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(200).WithMessage("Password cannot be longer than 200 characters")
                .Must((model, password) => model.PasswordVerification == password).WithMessage("Passwords don't match!");

            RuleFor(x => x.BirthDay)
                .NotEmpty().WithMessage("Birth date is required")
                .Must((model, birthDate) => birthDate < DateTime.Now.AddYears(-14)).WithMessage("You must be at least 14 years old to register!");

        }
    }
}
