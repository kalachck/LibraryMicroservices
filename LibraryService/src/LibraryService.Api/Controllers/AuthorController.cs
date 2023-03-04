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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService,
            IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var author = await _authorService.GetAsync(id);

                return Ok(author);
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
        public async Task<IActionResult> Add([FromQuery] AuthorRequestModel model)
        {
            try
            {
                var result = await _authorService.AddAsync(_mapper.Map<AuthorDTO>(model));

                return Ok(result);
            }
            catch (Exception)
            {
                return Conflict("The record was not added. There were technical problems");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] AuthorRequestModel model)
        {
            try
            {
                var result = await _authorService.UpdateAsync(id, _mapper.Map<AuthorDTO>(model));

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
                var result = await _authorService.DeleteAsync(id);

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
