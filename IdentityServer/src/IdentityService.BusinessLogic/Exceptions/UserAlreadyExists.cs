namespace IdentityService.BusinessLogic.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists(string message) : base(message)
        { }
    }
}
