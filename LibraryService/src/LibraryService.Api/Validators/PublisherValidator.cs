using LibrarySevice.Api.Models;
using FluentValidation;

namespace LibrarySevice.Api.Validators
{
    public class PublisherValidator : AbstractValidator<PublisherModel>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Address).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
