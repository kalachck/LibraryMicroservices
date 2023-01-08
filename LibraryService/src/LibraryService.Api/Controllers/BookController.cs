using AutoMapper;
using LibrarySevice.Api.Controllers.Abstract;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySevice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController<BookModel, BookDTO, BussinesLogic.Services.BookService>
    {
        public BookController(BussinesLogic.Services.BookService service, 
            IMapper mapper, 
            IValidator<BookModel> validator) : base(service, mapper, validator)
        { }
    }
}
