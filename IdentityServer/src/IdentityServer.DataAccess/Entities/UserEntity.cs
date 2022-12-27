using IdentityServer.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.DataAccess.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }
    }
}
