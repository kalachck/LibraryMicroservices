using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;

namespace LibrarySevice.BussinesLogic.Services
{
    public class AuthorService : BaseDtoService<AuthorDTO, AuthorEntity, AuthorRepository>
    {
        public AuthorService(AuthorRepository repository, IMapper mapper) : base(repository, mapper)
        { }
    }
}
