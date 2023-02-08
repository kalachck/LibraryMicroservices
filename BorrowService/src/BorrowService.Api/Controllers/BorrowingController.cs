using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BorrowService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingComponent _component;
        private readonly HttpClient _client;

        public BorrowingController(IBorrowingComponent component,
            IHttpClientFactory httpClientFactory)
        {
            _component = component;
            _client = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var borrowing = await _component.GetAsync(id);

                return Ok(borrowing);
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
        [Route("GetByBookId")]
        public async Task<IActionResult> GetByBookId(int id)
        {
            try
            {
                var borrowing = await _component.GetByBookIdAsync(id);

                return Ok(borrowing);
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
        [Route("GetByEmail")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                var borrowing = await _component.GetByEmailAsync(email);

                return Ok(borrowing);
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
        [Route("Borrow")]
        public async Task<IActionResult> Borrow(string email, int bookId)
        {
            try
            {
                var result = await _component.BorrowAsync(email, bookId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }

                if (ex is NotAvailableException)
                {
                    return NotFound(ex.Message);
                }

                return Conflict("The record was not borrowed. There were technical problems");
            }
        }

        [HttpPut]
        [Route("Extend")]
        public async Task<IActionResult> Extend(string email, int bookId)
        {
            try
            {
                var result = await _component.ExtendAsync(email, bookId);

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
                var result = await _component.DeleteAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }

                return Conflict("The record was not added. There were technical problems");
            }
        }
    }
}
