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
            CreateMap<PublisherRequestModel, PublisherDTO>()
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Books, options => options.MapFrom(src => src.Books));

            CreateMap<PublisherDTO, PublisherRequestModel>()
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Books, options => options.MapFrom(src => src.Books));

            CreateMap<PublisherDTO, PublisherEntity>()
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Books, options => options.MapFrom(src => src.Books))
                .ForMember(dest => dest.Id, options => options.Ignore());

            CreateMap<PublisherEntity, PublisherDTO>()
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => src.Address))
                .ForMember(dest => dest.Books, options => options.MapFrom(src => src.Books))
                .ForSourceMember(dest => dest.Id, options => options.DoNotValidate());
        }
    }
}
