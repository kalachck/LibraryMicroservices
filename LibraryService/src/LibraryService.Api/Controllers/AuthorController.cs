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
    public class AuthorController : BaseController<AuthorModel, AuthorDTO, AuthorService>
    {
        public AuthorController(AuthorService service,
            IMapper mapper,
            IValidator<AuthorModel> validator) : base(service, mapper, validator)
        { }
    }
}
