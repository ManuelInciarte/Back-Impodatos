using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impodatos.Services.Queries.DTOs
{
    public partial class AddEventDto
    {
        public string Event { get; set; }
        public string Status { get; set; }
        public string Program { get; set; }
        public string ProgramStage { get; set; }
        public string Enrollment { get; set; }
        public string EnrollmentStatus { get; set; }
        public string OrgUnit { get; set; }
        public string TrackedEntityInstance { get; set; }
        public List<object> Relationships { get; set; }
        public DateTimeOffset EventDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string StoredBy { get; set; }
        public List<DataValue> DataValues { get; set; }
        public List<object> Notes { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset Created { get; set; }
        public AtedByUserInfo CreatedByUserInfo { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public AtedByUserInfo LastUpdatedByUserInfo { get; set; }
        public string AttributeOptionCombo { get; set; }
        public string AttributeCategoryOptions { get; set; }
    }

    public partial class AtedByUserInfo
    {
        public string Uid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }

    public partial class DataValue
    {
        public string Value { get; set; }
        public string DataElement { get; set; }
        public bool ProvidedElsewhere { get; set; }
    }
}
