using AutoMapper;
using BorrowService.Api.RequestModels;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Options;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace BorrowService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingComponent _component;
        private readonly IMapper _mapper;
        private readonly IValidator<BorrowingRequestModel> _validator;
        private readonly HttpClient _client;

        public BorrowingController(IBorrowingComponent component,
            IMapper mapper, IValidator<BorrowingRequestModel> validator,
            IHttpClientFactory httpClientFactory)
        {
            _component = component;
            _mapper = mapper;
            _validator = validator;
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
            catch (SqlException)
            {
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
            catch (SqlException)
            {
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
            catch (SqlException)
            {
                return Conflict("Can't get this record. There were technical problems");
            }
        }

        [HttpPost]
        [Route("Borrow")]
        public async Task<IActionResult> Borrow(string email, int bookId)
        {
            try
            {
                var result = await _component.BorrowAsync(email, bookId, _client);

                return Ok(result);
            }
            catch (SqlException)
            {
                return Conflict("The record was not borrowed. There were technical problems");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] BorrowingRequestModel model)
        {
            try
            {
                await _validator.ValidateAsync(model);

                var borrowing = _mapper.Map<Borrowing>(model);

                var result = await _component.UpdateAsync(id, borrowing);

                return Ok(result);
            }
            catch (SqlException)
            {
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
            catch (SqlException)
            {
                return Conflict("The record was not added. There were technical problems");
            }
        }
    }
}
