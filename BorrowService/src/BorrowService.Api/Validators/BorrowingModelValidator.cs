using BorrowService.Api.Models;
using FluentValidation;

namespace BorrowService.Api.Validators
{
    public class BorrowingModelValidator : AbstractValidator<BorrowingModel>
    {
        public BorrowingModelValidator()
        {
            RuleFor(x => x.UserEmail).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.BookId).NotNull().NotEmpty().NotEqual(0);
            RuleFor(x => x.AddingDate).NotNull().NotEmpty();
            RuleFor(x => x.ExpirationDate).NotNull().NotEmpty().GreaterThan(x => x.AddingDate);
        }
    }
}
