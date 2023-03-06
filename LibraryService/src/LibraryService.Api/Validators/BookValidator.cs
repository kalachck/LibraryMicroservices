using LibraryService.Api.Models;
using LibraryService.BussinesLogic.Validators;
using LibraryService.BussinesLogic.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Api.Validators
{
    public class BookValidator : IValidator<BookRequestModel>
    {
        public async Task<RuleBuilder<BookRequestModel, string>> RuleForTitle()
        {
            return (RuleBuilder<BookRequestModel, string>)
                await Task.FromResult(new RuleBuilder<BookRequestModel, string>(x => x.Title)
                .MustAsync(async name => await Task.FromResult(!string.IsNullOrWhiteSpace(name))).Result
                .MustAsync(async name => await Task.FromResult(name.Length <= 50)).Result);
        }

        public async Task ValidateAsync(BookRequestModel model)
        {
            var rule = await RuleForTitle();

            if (!await rule.ValidateAsync(model))
            {
                throw new ValidationException("Model is not valid. It must not be empty and more then 50 characters");
            }
        }
    }
}
