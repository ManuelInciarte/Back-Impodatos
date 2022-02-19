using FluentValidation;
using Impodatos.Services.EventHandlers.Commands;
using Impodatos.Services.EventHandlers.Validators;
using Impodatos.Services.Queries;
using Impodatos.Services.Queries.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Impodatos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryQueryService _historyQueryService1;
        private readonly IMediator _mediator;
        private readonly IValidator<HistoryCreateCommand> _historyValidator;
       public HistoryController(IHistoryQueryService historyQueryService, IMediator mediator, IValidator<HistoryCreateCommand> historyValidator)
        {
            _historyQueryService1 = historyQueryService;
            _mediator = mediator;
            _historyValidator = historyValidator;
        }
        [HttpGet]
        public async Task<IEnumerable<HistoryDto>> GetAll()
        {
            return await _historyQueryService1.GetAllAsync();
        }
        [HttpGet("user")]
        public async Task<IEnumerable<HistoryDto>> GetAll(string user)
        {
            return await _historyQueryService1.GetHistoryUserAsync(user);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]HistoryCreateCommand command)
        {
            var validation = _historyValidator.Validate(command);
            if (validation.IsValid)
            {
                await _mediator.Publish(command);
                return Ok();
            }
            return Ok(validation.Errors);
    
        }
        [HttpPut]
        public async Task<IActionResult> Update(HistoryUpdateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }


    }
}
