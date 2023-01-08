using LibrarySevice.Api.Models;
using FluentValidation;

namespace LibrarySevice.Api.Validators
{
    public class BookAuthorValidator : AbstractValidator<BookAuthorModel>
    {
        public BookAuthorValidator()
        {
            RuleFor(x => x.Book).NotNull();
            RuleFor(x => x.Author).NotNull();
        }
    }
}
