using LibraryService.Api.Models;
using FluentValidation;

namespace LibraryService.Api.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorRequestModel>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(30);
        }
    }
}
