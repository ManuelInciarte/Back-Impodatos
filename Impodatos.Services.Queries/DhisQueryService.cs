using Impodatos.Services.Common.Security;
using Impodatos.Services.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace Impodatos.Services.Queries
{
    public interface IDhisQueryService
    {
        Task<DhisProgramDto> GetAllProgramAsync();
        Task<OrganisationUnitsDto> GetAllOrganisation();
        Task<UidGeneratedDto> GetUidGenerated(string quantity);
        Task<AddTracketResultDto> AddTracked(AddTrackedDto request);
        Task<EnrollmentDto> Enrollment(EnrollmentDto request);
        Task<dynamic> AddEvent(AddEventDto request);

    }
    public class DhisQueryService : IDhisQueryService
    {
        public async Task<DhisProgramDto> GetAllProgramAsync()
        {
            var result = await RequestHttp.CallMethod("dhis", "program");           
             return JsonConvert.DeserializeObject<DhisProgramDto>(result);
        }
        public async Task<OrganisationUnitsDto> GetAllOrganisation()
        {
            var result = await RequestHttp.CallMethod("dhis", "organisationUnits");
            return JsonConvert.DeserializeObject<OrganisationUnitsDto>(result);
        } 
        public async Task<UidGeneratedDto> GetUidGenerated(string quantity)
        {
            var result = await RequestHttp.CallGetMethod("dhis", "uidGenerated", quantity);
            return JsonConvert.DeserializeObject<UidGeneratedDto>(result);
        }
        public async Task<AddTracketResultDto> AddTracked(AddTrackedDto request)
        {
            var content = JsonConvert.SerializeObject(request);
            var result = await RequestHttp.CallMethod("dhis", "addTracked", content);
            return JsonConvert.DeserializeObject<AddTracketResultDto>(result);
        }
        public async Task<EnrollmentDto> Enrollment(EnrollmentDto request)
        {
            var content = JsonConvert.SerializeObject(request);
            var result = await RequestHttp.CallMethod("dhis", "enrollments", content);
            return JsonConvert.DeserializeObject<EnrollmentDto>(result);
        }
        public async Task<dynamic> AddEvent (AddEventDto request)
        {
            var content = JsonConvert.SerializeObject(request);
            var result = await RequestHttp.CallMethod("dhis", "trackerEvents", content);
            return JsonConvert.DeserializeObject<dynamic>(result);
        }
    }
}
