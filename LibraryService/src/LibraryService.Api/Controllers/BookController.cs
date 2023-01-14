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
        [Route("Take")]
        public async Task<IActionResult> Take(int amount)
        {
            var books = await _bookService.TakeAsync(amount);

            return Ok(books);
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
            var validationResult = await _bookValidator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var book = await _bookService.AddAsync(_mapper.Map<BookDTO>(model));

                return Ok(book);
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int id, [FromQuery] BookRequestModel model)
        {
            var validationResult = await _bookValidator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var book = _mapper.Map<BookDTO>(model);

                return Ok(await _bookService.UpdateAsync(id, book));
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.DeleteAsync(id);

            return Ok(book);
        }
    }
}
