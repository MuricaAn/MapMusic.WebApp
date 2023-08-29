
using FluentValidation;
using MapMusic.BusinessLogic.Implementation.Account.Models;

namespace MapMusic.BusinessLogic.Implementation.Account.Validations
{
    public  class RegisterArtistValidator : AbstractValidator<RegisterArtistModel>
    {
        public RegisterArtistValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(200).Must((model, password) => model.PasswordVerification == password).WithMessage("Passwords don't match!");
            RuleFor(x => x.StageName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.StageName).MaximumLength(50);
            RuleFor(x => x.ArtistType).NotEqual(0).WithMessage("Choose a type of artist");
        }
    }
}
