using LibraryService.Api.Models;
using FluentValidation;

namespace LibraryService.Api.Validators
{
    public class BookValidator : AbstractValidator<BookRequestModel>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.PublicationDate).NotNull();
        }
    }
}
