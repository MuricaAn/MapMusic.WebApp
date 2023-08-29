using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Event.Validations
{
    public class EditEventValidator : AbstractValidator<EditEventModel>
    {
        public EditEventValidator ()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Start date cannot be in the past");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date cannot be before start date");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");

            RuleFor(x => x.ProfilePhoto)
                .NotEmpty().WithMessage("Image is required");

            RuleFor(x => x.MusicTypeId)
                .NotEmpty().WithMessage("Music type is required");

            RuleFor(x => x.LocationId)
                .NotEmpty().WithMessage("Location is required");
        }
    }
}
