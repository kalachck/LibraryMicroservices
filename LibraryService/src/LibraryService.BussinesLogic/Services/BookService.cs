using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;

namespace LibrarySevice.BussinesLogic.Services
{
    public class BookService : BaseDtoService<BookDTO, BookEntity, BookRepository>
    {
        public BookService(BookRepository repository, IMapper mapper) : base(repository, mapper)
        { }
    }
}
