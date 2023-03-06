using LibraryService.Api.Models;
using LibraryService.BussinesLogic.Validators;
using LibraryService.BussinesLogic.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Api.Validators
{
    public class AuthorValidator : IValidator<AuthorRequestModel>
    {
        public async Task<RuleBuilder<AuthorRequestModel, string>> RuleForName()
        {
            return (RuleBuilder<AuthorRequestModel, string>)
                await Task.FromResult(new RuleBuilder<AuthorRequestModel, string>(x => x.Name)
                .MustAsync(async name => await Task.FromResult(!string.IsNullOrWhiteSpace(name))).Result
                .MustAsync(async name => await Task.FromResult(name.Length <= 30)).Result);
        }

        public async Task ValidateAsync(AuthorRequestModel model)
        {
            var rule = await RuleForName();

            if (!await rule.ValidateAsync(model))
            {
                throw new ValidationException("Model is not valid. It must not be empty and more then 30 characters");
            }
        }
    }
}
