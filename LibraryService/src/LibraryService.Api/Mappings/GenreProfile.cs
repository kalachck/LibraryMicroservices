using AutoMapper;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.DataAccess.Entities;

namespace LibrarySevice.Api.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreRequestModel, GenreDTO>();

            CreateMap<GenreDTO, GenreRequestModel>();

            CreateMap<GenreDTO, GenreEntity>()
                .ForMember(dest => dest.Id, options => options.Ignore());

            CreateMap<GenreEntity, GenreDTO>()
                .ForSourceMember(dest => dest.Id, options => options.DoNotValidate()); ;
        }
    }
}
