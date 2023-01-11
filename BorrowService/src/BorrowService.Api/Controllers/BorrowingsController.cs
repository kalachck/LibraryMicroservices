using AutoMapper;
using BorrowService.Api.RequestModels;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BorrowService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingComponent _component;
        private readonly IMapper _mapper;
        private readonly IValidator<BorrowingRequestModel> _validator;

        public BorrowingsController(IBorrowingComponent component,
            IMapper mapper, IValidator<BorrowingRequestModel> validator)
        {
            _component = component;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("Take")]
        public async Task<IActionResult> Take(int amount)
        {
            var borrowings = await _component.TakeAsync(amount);

            return Ok(borrowings);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var borrowing = await _component.GetAsync(id);

            return Ok(borrowing);
        }

        [HttpGet]
        [Route("GetByBookId")]
        public async Task<IActionResult> GetByBookId(int id)
        {
            var borrowing = await _component.GetByBookIdAsync(id);

            return Ok(borrowing);
        }

        [HttpGet]
        [Route("GetByEmail")]
        public async Task<IActionResult> Get(string email)
        {
            var borrowing = await _component.GetByEmailAsync(email);

            return Ok(borrowing);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] BorrowingRequestModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var borrowing = await _component.AddAsync(_mapper.Map<BorrowingEntity>(model));

                return Ok(borrowing);
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] BorrowingRequestModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var borrowing = _mapper.Map<BorrowingEntity>(model);

                return Ok(await _component.UpdateAsync(id, borrowing));
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrowing = await _component.DeleteAsync(id);

            return Ok(borrowing);
        }
    }
}
