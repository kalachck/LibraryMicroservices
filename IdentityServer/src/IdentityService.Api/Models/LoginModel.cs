using IdentityService.BusinessLogic.Validators;

namespace IdentityService.Api.Models
{
    public class LoginModel
    {
        [NotNullOrEmpty]
        [HasMaxLength(30)]
        public string UserName { get; set; }

        [NotNullOrEmpty]
        [HasMaxLength(30)]
        public string Email { get; set; }

        [NotNullOrEmpty]
        [HasMaxLength(30)]
        public string Password { get; set; }
    }
}
