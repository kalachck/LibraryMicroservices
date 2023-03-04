using BorrowService.Api.Models;
using BorrowService.Borrowings.Components.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BorrowService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingComponent _component;

        public BorrowingController(IBorrowingComponent component)
        {
            _component = component;
        }

        [HttpGet]
        [Route("GetByEmail")]
        public async Task<IActionResult> Get([FromQuery] BorrowingRequestModel model)
        {
            var borrowing = await _component.GetAsync(model.UserEmail, model.BookTitle);

            return Ok(borrowing);
        }

        [HttpPost]
        [Route("Borrow")]
        public async Task<IActionResult> Borrow([FromQuery] BorrowingRequestModel model, int borrowingPeriod)
        {
            var result = await _component.BorrowAsync(model.UserEmail, model.BookTitle, borrowingPeriod);

            return Ok(result);
        }

        [HttpPut]
        [Route("Extend")]
        public async Task<IActionResult> Extend([FromQuery] BorrowingRequestModel model, int borrowingPeriod)
        {
            var result = await _component.ExtendAsync(model.UserEmail, model.BookTitle, borrowingPeriod);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] BorrowingRequestModel model)
        {
            var result = await _component.DeleteAsync(model.UserEmail, model.BookTitle);

            return Ok(result);
        }
    }
}
