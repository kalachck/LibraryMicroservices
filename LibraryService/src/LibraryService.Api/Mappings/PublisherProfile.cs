using AutoMapper;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.DataAccess.Entities;

namespace LibrarySevice.Api.Mappings
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherRequestModel, PublisherDTO>();

            CreateMap<PublisherDTO, PublisherRequestModel>();

            CreateMap<PublisherDTO, PublisherEntity>()
                .ForMember(dest => dest.Id, options => options.Ignore());

            CreateMap<PublisherEntity, PublisherDTO>()
                .ForSourceMember(dest => dest.Id, options => options.DoNotValidate());
        }
    }
}
