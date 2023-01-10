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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IValidator<AuthorRequestModel> _validator;

        public AuthorController(IAuthorService authorService, IMapper mapper, IValidator<AuthorRequestModel> validator)
        {
            _authorService = authorService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("Take")]
        public async Task<IActionResult> Take(int amount)
        {
            var authors = await _authorService.TakeAsync(amount);

            return Ok(authors);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var author = await _authorService.GetAsync(id);

            return Ok(author);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] AuthorRequestModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var author = await _authorService.AddAsync(_mapper.Map<AuthorDTO>(model));

                return Ok(author);
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] AuthorRequestModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var author = _mapper.Map<AuthorDTO>(model);

                return Ok(await _authorService.UpdateAsync(id, author));
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _authorService.DeleteAsync(id);

            return Ok(dto);
        }
    }
}
