using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Validations
{
    public class MyArtistValidator : AbstractValidator<MyArtistProfileModel>
    {
        public MyArtistValidator()
        {
            RuleFor(x => x.CurrentPassword)
              .NotEmpty().WithMessage("To be able to change your profile insert your current password");
            RuleFor(x => x.StageName)
                .NotEmpty().WithMessage("Description is required");
        }
    }
}
