using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Event.Models;
using MapMusic.BusinessLogic.Implementation.PhotoImp.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.PhotoImp.Validations
{
    public class AddPhotoValidator : AbstractValidator<ShowEventPhotoModel>
    {
        public AddPhotoValidator()
        {
            RuleFor(x => x.PhotoDescription)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(50).WithMessage("Photo description cannot be longer than 50 characters");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Image is required");
        }
    }
}
