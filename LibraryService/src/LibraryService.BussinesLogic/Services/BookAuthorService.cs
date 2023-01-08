using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;

namespace LibrarySevice.BussinesLogic.Services
{
    public class BookAuthorService : BaseDtoService<BookAuthorDTO, BookAuthorEntity, BookAuthorRepository>
    {
        public BookAuthorService(BookAuthorRepository repository, IMapper mapper) : base(repository, mapper)
        { }
    }
}
