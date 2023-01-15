using AutoMapper;
using FluentValidation;
using LibrarySevice.Api.Models;
using LibrarySevice.BussinesLogic.DTOs;
using LibrarySevice.BussinesLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

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
            var genre = await _genreService.GetAsync(id);

            return Ok(genre);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] GenreRequestModel model)
        {
            await _validator.ValidateAsync(model);

            var result = await _genreService.AddAsync(_mapper.Map<GenreDTO>(model));

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] GenreRequestModel model)
        {
            await _validator.ValidateAsync(model);

            var result = await _genreService.UpdateAsync(id, _mapper.Map<GenreDTO>(model));

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _genreService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
