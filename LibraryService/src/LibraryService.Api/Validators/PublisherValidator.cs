using LibraryService.Api.Models;
using LibraryService.BussinesLogic.Validators;
using LibraryService.BussinesLogic.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Api.Validators
{
    public class PublisherValidator : IValidator<PublisherRequestModel>
    {
        public async Task<RuleBuilder<PublisherRequestModel, string>> RuleForName()
        {
            return (RuleBuilder<PublisherRequestModel, string>)
                await Task.FromResult(new RuleBuilder<PublisherRequestModel, string>(x => x.Name)
                .MustAsync(async name => await Task.FromResult(!string.IsNullOrWhiteSpace(name))).Result
                .MustAsync(async name => await Task.FromResult(name.Length <= 30)).Result);
        }

        public async Task<RuleBuilder<PublisherRequestModel, string>> RuleForAddress()
        {
            return (RuleBuilder<PublisherRequestModel, string>)
                await Task.FromResult(new RuleBuilder<PublisherRequestModel, string>(x => x.Address)
                .MustAsync(async address => await Task.FromResult(!string.IsNullOrWhiteSpace(address))).Result
                .MustAsync(async address => await Task.FromResult(address.Length <= 30)).Result);
        }

        public async Task ValidateAsync(PublisherRequestModel model)
        {
            var rules = new List<RuleBuilder<PublisherRequestModel, string>>()
            {
                await RuleForName(),
                await RuleForAddress(),
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
