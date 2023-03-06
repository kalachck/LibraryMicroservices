using AutoMapper;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.BussinesLogic.Validators.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IValidator<BookRequestModel> _validator;

        public BookController(IBookService bookService,
            IMapper mapper,
            IValidator<BookRequestModel> validator)
        {
            _bookService = bookService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetAsync(id);

            return Ok(book);
        }

        [HttpGet]
        [Route("GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var book = await _bookService.GetByTitleAsync(title);

            return Ok(book);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] BookRequestModel model)
        {
            await _validator.ValidateAsync(model);

            var result = await _bookService.AddAsync(_mapper.Map<BookDTO>(model));

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int id, [FromQuery] BookRequestModel model)
        {
            await _validator.ValidateAsync(model);

            var result = await _bookService.UpdateAsync(id, _mapper.Map<BookDTO>(model));

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
