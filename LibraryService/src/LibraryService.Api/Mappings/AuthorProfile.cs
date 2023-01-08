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
            CreateMap<AuthorDTO, AuthorEntity>().ReverseMap();
            CreateMap<AuthorModel, AuthorDTO>().ReverseMap();
        }
    }
}
