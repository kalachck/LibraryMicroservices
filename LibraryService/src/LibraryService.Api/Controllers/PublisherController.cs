using AutoMapper;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.BussinesLogic.Validators.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        private readonly IValidator<PublisherRequestModel> _validator;

        public PublisherController(IPublisherService publisherService,
            IMapper mapper,
            IValidator<PublisherRequestModel> validator)
        {
            _publisherService = publisherService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var publisher = await _publisherService.GetAsync(id);

            return Ok(publisher);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] PublisherRequestModel model)
        {
            await _validator.ValidateAsync(model);

            var result = await _publisherService.AddAsync(_mapper.Map<PublisherDTO>(model));

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] PublisherRequestModel model)
        {
            await _validator.ValidateAsync(model);

            var result = await _publisherService.UpdateAsync(id, _mapper.Map<PublisherDTO>(model));

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _publisherService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
