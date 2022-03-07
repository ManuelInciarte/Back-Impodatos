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
    public class DhisIntegrationController : ControllerBase
    {
        private readonly IDhisQueryService _dhisQueryService;
        private readonly IMediator _mediator;
        public DhisIntegrationController(IDhisQueryService dhisQueryService, IMediator mediator)
        {
            _dhisQueryService = dhisQueryService;
            _mediator = mediator;
        }


        [HttpGet]
        [Route("getAllProgram")]
        public async Task<DhisProgramDto> GetAllProgram()
        {         
            return await _dhisQueryService.GetAllProgramAsync();
        }

        [HttpGet]
        [Route("getAllOrganisationUnits")]
        public async Task<OrganisationUnitsDto> GetAllOrganisationUnits()
        {
            return await _dhisQueryService.GetAllOrganisation();
        }

        [HttpGet]
        [Route("getUidGenerated/{quantity}")]
        public async Task<UidGeneratedDto> GetUidGenerated(string quantity)
        {
            return await _dhisQueryService.GetUidGenerated(quantity);
        }  
        
        [HttpPost]
        [Route("addTracket")]
        public async Task<AddTracketResultDto> AddTracket(AddTrackedDto request)
        {
            return await _dhisQueryService.AddTracked(request);
        }
        [HttpPost]
        [Route("enrollment")]
        public async Task<EnrollmentDto> Enrollment(EnrollmentDto request)
        {
            return await _dhisQueryService.Enrollment(request);
        }
        [HttpPost]
        [Route("addEvent")]
        public async Task<dynamic> AddEvent(AddEventDto request)
        {
            return await _dhisQueryService.AddEvent(request);
        }
    }
}
