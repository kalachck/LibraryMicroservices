using AutoMapper;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.DataAccess.Entities;

namespace LibrarySevice.Api.Mappings
{
    public class BookAuthorProfile : Profile
    {
        public BookAuthorProfile()
        {
            CreateMap<BookAuthorDTO, BookAuthorEntity>().ReverseMap();
            CreateMap<BookAuthorDTO, BookAuthorModel>().ReverseMap();
        }
    }
}
