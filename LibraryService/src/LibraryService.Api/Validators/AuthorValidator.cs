using LibrarySevice.Api.Models;
using FluentValidation;

namespace LibrarySevice.Api.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorModel>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.Surname).NotNull().NotEmpty().MaximumLength(30);
        }
    }
}
