using AutoMapper;
using FluentValidation;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Exceptions;
using LibrarySevice.BussinesLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LibrarySevice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly IValidator<GenreRequestModel> _validator;

        public GenreController(IGenreService genreService, 
            IMapper mapper, 
            IValidator<GenreRequestModel> validator)
        {
            _genreService = genreService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var genre = await _genreService.GetAsync(id);

                return Ok(genre);
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
        public async Task<IActionResult> Add([FromQuery] GenreRequestModel model)
        {
            try
            {
                await _validator.ValidateAsync(model);

                var result = await _genreService.AddAsync(_mapper.Map<GenreDTO>(model));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict("The record was not added. There were technical problems");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] GenreRequestModel model)
        {
            try
            {
                await _validator.ValidateAsync(model);

                var result = await _genreService.UpdateAsync(id, _mapper.Map<GenreDTO>(model));

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
                var result = await _genreService.DeleteAsync(id);

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
