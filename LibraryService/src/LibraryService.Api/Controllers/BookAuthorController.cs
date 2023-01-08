using AutoMapper;
using LibrarySevice.Api.Controllers.Abstract;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySevice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorController : BaseController<BookAuthorModel, BookAuthorDTO, BookAuthorService>
    {
        public BookAuthorController(BookAuthorService service,
            IMapper mapper,
            IValidator<BookAuthorModel> validator) : base(service, mapper, validator)
        { }
    }
}
