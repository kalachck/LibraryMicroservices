using BorrowService.Api.Models;
using BorrowService.Borrowings.Validators;
using BorrowService.Borrowings.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace BorrowService.Api.Validators
{
    public class BorrowingValidator : IValidator<BorrowingRequestModel>
    {
        public async Task<RuleBuilder<BorrowingRequestModel, string>> RuleForUserEmail()
        {
            return (RuleBuilder<BorrowingRequestModel, string>)
                await Task.FromResult(new RuleBuilder<BorrowingRequestModel, string>(x => x.UserEmail)
                .MustAsync(async email => await Task.FromResult(!string.IsNullOrWhiteSpace(email))).Result);
        }

        public async Task<RuleBuilder<BorrowingRequestModel, string>> RuleForBookTitle()
        {
            return (RuleBuilder<BorrowingRequestModel, string>)
                await Task.FromResult(new RuleBuilder<BorrowingRequestModel, string>(x => x.BookTitle)
                .MustAsync(async title => await Task.FromResult(!string.IsNullOrWhiteSpace(title))).Result
                .MustAsync(async title => await Task.FromResult(title.Length <= 50)).Result);
        }

        public async Task ValidateAsync(BorrowingRequestModel model)
        {
            var rules = new List<RuleBuilder<BorrowingRequestModel, string>>()
            {
                await RuleForUserEmail(),
                await RuleForBookTitle(),
            };

            foreach (var rule in rules)
            {
                if (!await rule.ValidateAsync(model))
                {
                    throw new ValidationException("Model is not valid. It must not be empty and more then 30 characters");
                }
            }
        }
    }
}
