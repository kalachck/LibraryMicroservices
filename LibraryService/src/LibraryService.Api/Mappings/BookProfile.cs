using AutoMapper;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.DataAccess.Entities;

namespace LibrarySevice.Api.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookRequestModel, BookDTO>();

            CreateMap<BookDTO, BookRequestModel>();

            CreateMap<BookDTO, BookEntity>();

            CreateMap<BookEntity, BookDTO>();
        }
    }
}
