using FluentValidation;
using LibrarySevice.Api.Models;

namespace LibrarySevice.Api.Validators
{
    public class GenreValidator : AbstractValidator<GenreRequestModel>
    {
        public GenreValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(30);
        }
    }
}
