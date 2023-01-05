namespace IdentityService.BusinessLogic.Services.Abstarct
{
    public interface IMailService
    {
        Task SendMessageAsync(string email);
    }
}
