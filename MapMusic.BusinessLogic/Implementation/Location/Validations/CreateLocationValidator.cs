using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Location.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Location.Validations
{
    public class CreateLocationValidator : AbstractValidator<CreateLocationModel>
    {
        public CreateLocationValidator ()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Location name cannot be longer than 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Location description cannot be longer than 1000 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");
        }
    }
}
