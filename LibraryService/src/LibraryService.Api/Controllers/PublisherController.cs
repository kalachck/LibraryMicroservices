using AutoMapper;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService,
            IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var publisher = await _publisherService.GetAsync(id);

                return Ok(publisher);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }

                return Conflict("Can't get this record. There were technical problems");
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] PublisherRequestModel model)
        {
            try
            {
                var result = await _publisherService.AddAsync(_mapper.Map<PublisherDTO>(model));

                return Ok(result);
            }
            catch (Exception)
            {
                return Conflict("The record was not added. There were technical problems");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] PublisherRequestModel model)
        {
            try
            {
                var result = await _publisherService.UpdateAsync(id, _mapper.Map<PublisherDTO>(model));

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }

                return Conflict("The record was not updated. There were technical problems");
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _publisherService.DeleteAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }

                return Conflict("The record was not deleted. There were technical problems");
            }
        }
    }
}
