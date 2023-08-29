using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapMusic.BusinessLogic.Implementation.Account.Validations
{
    public class MyOrganizerValidator : AbstractValidator<MyOrganizerProfileModel>
    {
        public MyOrganizerValidator()
        {
            RuleFor(x => x.CurrentPassword)
               .NotEmpty().WithMessage("To be able to change your profile insert your current password");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Description is required");
        }
    }
}
