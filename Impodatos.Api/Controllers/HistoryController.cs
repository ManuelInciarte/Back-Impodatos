using Impodatos.Domain;
using Impodatos.Services.Queries;
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
       public HistoryController(IHistoryQueryService historyQueryService)
        {
            _historyQueryService1 = historyQueryService;
        }
        [HttpGet]
        public async Task<IEnumerable<History>> GetAll()
        {
            return await _historyQueryService1.GetAllAsync();
        }

    }
}
