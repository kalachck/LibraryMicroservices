using AutoMapper;
using FluentValidation;
using LibraryService.Api.Models;
using LibraryService.BussinesLogic.DTOs;
using LibraryService.BussinesLogic.Exceptions;
using LibraryService.BussinesLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Api.Controllers
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
            try
            {
                var book = await _bookService.GetAsync(id);

                return Ok(book);
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
            try
            {
                await _bookValidator.ValidateAsync(model);

                var result = await _bookService.AddAsync(_mapper.Map<BookDTO>(model));

                return Ok(result);
            }
            catch (Exception)
            {
                return Conflict("The record was not added. There were technical problems");
            }
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int id, [FromQuery] BookRequestModel model)
        {
            try
            {
                await _bookValidator.ValidateAsync(model);

                var result = await _bookService.UpdateAsync(id, _mapper.Map<BookDTO>(model));

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
                var result = await _bookService.DeleteAsync(id);

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
