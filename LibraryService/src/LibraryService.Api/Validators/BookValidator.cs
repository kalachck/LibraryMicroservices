using LibrarySevice.Api.Models;
using FluentValidation;

namespace LibrarySevice.Api.Validators
{
    public class BookValidator : AbstractValidator<BookModel>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Description).Empty().Null().MaximumLength(100);
            RuleFor(x => x.PublicationDate).NotNull();
            RuleFor(x => x.PageCount).GreaterThan(50);
            RuleFor(x => x.PublisherId).NotNull();
            RuleFor(x => x.Publisher).NotNull();
        }
    }
}
