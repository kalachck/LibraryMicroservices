namespace IdentityService.BusinessLogic.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message)
        { }
    }
}
