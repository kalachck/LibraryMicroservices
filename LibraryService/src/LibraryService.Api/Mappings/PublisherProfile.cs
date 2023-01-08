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
            CreateMap<PublisherDTO, PublisherEntity>().ReverseMap();
            CreateMap<PublisherDTO, PublisherModel>().ReverseMap();
        }
    }
}
