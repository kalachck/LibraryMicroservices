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

        public GenreController(IGenreService genreService, IMapper mapper, IValidator<GenreRequestModel> validator)
        {
            _genreService = genreService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("Take")]
        public async Task<IActionResult> Take(int amount)
        {
            var genres = await _genreService.TakeAsync(amount);

            return Ok(genres);
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
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var genre = await _genreService.AddAsync(_mapper.Map<GenreDTO>(model));

                return Ok(genre);
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] GenreRequestModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var genre = _mapper.Map<GenreDTO>(model);

                return Ok(await _genreService.UpdateAsync(id, genre));
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreService.DeleteAsync(id);

            return Ok(genre);
        }
    }
}
