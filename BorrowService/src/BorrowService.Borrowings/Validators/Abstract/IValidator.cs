namespace BorrowService.Borrowings.Validators.Abstract
{
    public interface IValidator<TModel>
    {
        Task ValidateAsync(TModel model);
    }
}
