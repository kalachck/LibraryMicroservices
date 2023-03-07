using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Validators;
using IdentityService.BusinessLogic.Validators.Abstract;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Api.Validators
{
    public class LoginModelValidator : IValidator<LoginModel>
    {
        public async Task<RuleBuilder<LoginModel, string>> RuleForUserName()
        {
            return (RuleBuilder<LoginModel, string>)
                await Task.FromResult(new RuleBuilder<LoginModel, string>(x => x.UserName)
                .MustAsync(async name => await Task.FromResult(!string.IsNullOrWhiteSpace(name))).Result
                .MustAsync(async name => await Task.FromResult(name.Length <= 30)).Result);
        }

        public async Task<RuleBuilder<LoginModel, string>> RuleForEmail()
        {
            return (RuleBuilder<LoginModel, string>)
                await Task.FromResult(new RuleBuilder<LoginModel, string>(x => x.Email)
                .MustAsync(async name => await Task.FromResult(!string.IsNullOrWhiteSpace(name))).Result
                .MustAsync(async name => await Task.FromResult(name.Length <= 30)).Result);
        }

        public async Task<RuleBuilder<LoginModel, string>> RuleForPassword()
        {
            return (RuleBuilder<LoginModel, string>)
                await Task.FromResult(new RuleBuilder<LoginModel, string>(x => x.Password)
                .MustAsync(async name => await Task.FromResult(!string.IsNullOrWhiteSpace(name))).Result
                .MustAsync(async name => await Task.FromResult(name.Length <= 30)).Result);
        }

        public async Task ValidateAsync(LoginModel model)
        {
            var rules = new Dictionary<string, RuleBuilder<LoginModel, string>>()
            {
                { nameof(model.UserName), await RuleForUserName() },
                { nameof(model.Email), await RuleForEmail() },
                { nameof(model.Password), await RuleForPassword() },
            };

            foreach (var rule in rules)
            {
                if (!await rule.Value.ValidateAsync(model))
                {
                    throw new ValidationException($"{rule.Key} is not valid. It must not be empty and more then 30 characters");
                }
            }
        }
    }
}
