using AutoMapper;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;

namespace LibrarySevice.BussinesLogic.Services
{
    public class PublisherService : BaseDtoService<PublisherDTO, PublisherEntity, PublisherRepository>
    {
        public PublisherService(PublisherRepository repository, IMapper mapper) : base(repository, mapper)
        { }
    }
}
