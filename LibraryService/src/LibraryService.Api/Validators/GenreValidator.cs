using FluentValidation;
using LibraryService.Api.Models;

namespace LibraryService.Api.Validators
{
    public class GenreValidator : AbstractValidator<GenreRequestModel>
    {
        public GenreValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(30);
        }
    }
}
