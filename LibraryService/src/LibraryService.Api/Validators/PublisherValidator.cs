using LibraryService.Api.Models;
using FluentValidation;

namespace LibraryService.Api.Validators
{
    public class PublisherValidator : AbstractValidator<PublisherRequestModel>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Address).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
