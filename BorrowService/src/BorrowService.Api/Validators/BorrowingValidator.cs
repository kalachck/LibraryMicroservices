using BorrowService.Api.Models;
using FluentValidation;

namespace BorrowService.Api.Validators
{
    public class BorrowingValidator : AbstractValidator<BorrowingRequestModel>
    {
        public BorrowingValidator()
        {
            RuleFor(x => x.UserEmail).NotNull().NotEmpty();
            RuleFor(x => x.BookTitle).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
