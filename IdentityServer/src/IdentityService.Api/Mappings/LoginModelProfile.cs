using AutoMapper;
using IdentityService.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Api.Mappings
{
    public class LoginModelProfile : Profile
    {
        public LoginModelProfile()
        {
            CreateMap<LoginModel, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
        }
    }
}
