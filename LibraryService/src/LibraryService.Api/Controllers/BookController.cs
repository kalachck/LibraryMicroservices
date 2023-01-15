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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IValidator<BookRequestModel> _bookValidator;

        public BookController(IBookService bookService,
            IMapper mapper,
            IValidator<BookRequestModel> bookValidator)
        {
            _bookService = bookService;
            _mapper = mapper;
            _bookValidator = bookValidator;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetAsync(id);

            return Ok(book);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] BookRequestModel model)
        {
            await _bookValidator.ValidateAsync(model);

            var result = await _bookService.AddAsync(_mapper.Map<BookDTO>(model));

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int id, [FromQuery] BookRequestModel model)
        {
            await _bookValidator.ValidateAsync(model);

            var result = await _bookService.UpdateAsync(id, _mapper.Map<BookDTO>(model))

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            return Ok(result);
        }
    }
}
