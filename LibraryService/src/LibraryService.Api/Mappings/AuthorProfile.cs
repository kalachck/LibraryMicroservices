using AutoMapper;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.DataAccess.Entities;

namespace LibraryService.Api.Mappings
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
