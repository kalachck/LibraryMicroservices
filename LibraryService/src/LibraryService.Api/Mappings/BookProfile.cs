using AutoMapper;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.DataAccess.Entities;

namespace LibraryService.Api.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookRequestModel, BookDTO>();

            CreateMap<BookDTO, BookRequestModel>();

            CreateMap<BookDTO, Book>();

            CreateMap<Book, BookDTO>();
        }
    }
}
