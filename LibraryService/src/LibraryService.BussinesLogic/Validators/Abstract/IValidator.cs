namespace LibraryService.BussinesLogic.Validators.Abstract
{
    public interface IValidator<TModel>
    {
        Task ValidateAsync(TModel model);
    }
}
