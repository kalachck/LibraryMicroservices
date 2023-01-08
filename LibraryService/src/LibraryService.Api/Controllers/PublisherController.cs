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
    public class PublisherController : BaseController<PublisherModel, PublisherDTO, PublisherService>
    {
        public PublisherController(PublisherService service,
            IMapper mapper,
            IValidator<PublisherModel> validator) : base(service, mapper, validator)
        { }
    }
}
