using AutoMapper;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.DataAccess.Entities;

namespace LibrarySevice.Api.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorRequestModel, AuthorDTO>();

            CreateMap<AuthorDTO, AuthorRequestModel>();

            CreateMap<AuthorDTO, Author>()
                .ForMember(dest => dest.Id, options => options.Ignore());

            CreateMap<Author, AuthorDTO>()
                .ForSourceMember(dest => dest.Id, options => options.DoNotValidate());
        }
    }
}
