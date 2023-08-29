using FluentValidation.Results;
using MapMusic.Common.Exceptions;

namespace MapMusic.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThenThrow(this ValidationResult result, object model)
        {
            if (!result.IsValid)
            {
                throw new ValidationErrorException(result, model);
            }
        }
    }
}
