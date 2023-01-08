using AutoMapper;
using LibrarySevice.Api.Models.Abstract;
using LibrarySevice.BussinesLogic.DTOs.Abstract;
using LibrarySevice.BussinesLogic.Services.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySevice.Api.Controllers.Abstract
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TModel, TDto, TService> : ControllerBase
        where TModel : BaseModel
        where TDto : BaseDTO
        where TService : IBaseDtoService<TDto>
    {
        private readonly TService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<TModel> _validator;

        public BaseController(TService service, IMapper mapper, IValidator<TModel> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [Route("Take")]
        public async Task<IActionResult> Take(int amount)
        {
            var dtos = await _service.TakeAsync(amount);

            return Ok(dtos);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _service.GetAsync(id);

            return Ok(dto);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromQuery] TModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var dto = await _service.UpsertAsync(_mapper.Map<TDto>(model));

                return Ok(dto);
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromQuery] TModel model)
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var dto = _mapper.Map<TDto>(model);

                dto.Id = id;

                return Ok(await _service.UpsertAsync(dto));
            }

            return BadRequest("Invalid data! Pleasy try again");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.DeleteAsync(id);

            return Ok(dto);
        }
    }
}
