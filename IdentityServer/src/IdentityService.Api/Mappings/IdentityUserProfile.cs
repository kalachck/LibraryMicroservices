using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Api.Mappings
{
    public class IdentityUserProfile : Profile
    {
        public IdentityUserProfile()
        {
            CreateMap<IdentityUser, IdentityUser>().ReverseMap();
        }
    }
}
