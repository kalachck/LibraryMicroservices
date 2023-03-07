namespace IdentityService.BusinessLogic.Validators.Abstract
{
    public interface IRuleBuilder<TModel, TProperty>
    {
        Task<IRuleBuilder<TModel, TProperty>> MustAsync(Func<TProperty, Task<bool>> validator);

        Task<bool> ValidateAsync(TModel model);
    }
}
