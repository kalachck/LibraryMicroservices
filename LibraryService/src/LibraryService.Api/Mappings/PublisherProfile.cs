using AutoMapper;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.DataAccess.Entities;

namespace LibraryService.Api.Mappings
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherRequestModel, PublisherDTO>();

            CreateMap<PublisherDTO, PublisherRequestModel>();

            CreateMap<PublisherDTO, Publisher>()
                .ForMember(dest => dest.Id, options => options.Ignore());

            CreateMap<Publisher, PublisherDTO>()
                .ForSourceMember(dest => dest.Id, options => options.DoNotValidate());
        }
    }
}
