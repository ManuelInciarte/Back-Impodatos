using Impodatos.Services.EventHandlers.Commands;
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
       public HistoryController(IHistoryQueryService historyQueryService, IMediator mediator)
        {
            _historyQueryService1 = historyQueryService;
            _mediator = mediator;
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
        public async Task<IActionResult> Add(HistoryCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(HistoryUpdateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }


    }
}
