using LibrarySevice.Api.Models;
using FluentValidation;

namespace LibrarySevice.Api.Validators
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
