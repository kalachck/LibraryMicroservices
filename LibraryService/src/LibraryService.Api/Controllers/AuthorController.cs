using AutoMapper;
using FluentValidation;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
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
        private readonly IValidator<AuthorRequestModel> _validator;

        public AuthorController(IAuthorService authorService,
            IMapper mapper,
            IValidator<AuthorRequestModel> validator)
        {
            _authorService = authorService;
            _mapper = mapper;
            _validator = validator;
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
            await _validator.ValidateAsync(model);

            var result = await _authorService.AddAsync(_mapper.Map<AuthorDTO>(model));

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] AuthorRequestModel model)
        {
            await _validator.ValidateAsync(model);

            var result = await _authorService.UpdateAsync(id, _mapper.Map<AuthorDTO>(model));

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _authorService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
