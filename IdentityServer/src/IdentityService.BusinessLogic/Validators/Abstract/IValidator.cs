namespace IdentityService.BusinessLogic.Validators.Abstract
{
    public interface IValidator<TModel>
    {
        Task ValidateAsync(TModel model);
    }
}
