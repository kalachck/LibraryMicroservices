using IdentityService.BusinessLogic.Validators.Abstract;

namespace IdentityService.BusinessLogic.Validators
{
    public class RuleBuilder<TModel, TProperty> : IRuleBuilder<TModel, TProperty>
    {
        private readonly Func<TModel, TProperty> _propertyFunc;
        private readonly List<Func<TProperty, Task<bool>>> _validators;

        public RuleBuilder(Func<TModel, TProperty> propertyFunc)
        {
            _propertyFunc = propertyFunc;
            _validators = new List<Func<TProperty, Task<bool>>>();
        }

        public async Task<IRuleBuilder<TModel, TProperty>> MustAsync(Func<TProperty, Task<bool>> validator)
        {
            _validators.Add(async property =>
            {
                return await validator(property);
            });

            return await Task.FromResult(this);
        }

        public async Task<bool> ValidateAsync(TModel model)
        {
            var value = _propertyFunc(model);

            foreach (var validator in _validators)
            {
                if (!await validator(value))
                {
                    return await Task.FromResult(false);
                }
            }

            return await Task.FromResult(true);
        }
    }
}
