using AutoMapper;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.DataAccess.Entities;

namespace LibraryService.Api.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreRequestModel, GenreDTO>();

            CreateMap<GenreDTO, GenreRequestModel>();

            CreateMap<GenreDTO, Genre>()
                .ForMember(dest => dest.Id, options => options.Ignore());

            CreateMap<Genre, GenreDTO>()
                .ForSourceMember(dest => dest.Id, options => options.DoNotValidate()); ;
        }
    }
}
