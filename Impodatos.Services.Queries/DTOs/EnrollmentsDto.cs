using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impodatos.Services.Queries.DTOs
{
    public partial class EnrollmentDto
    {
        public List<Enrollment> Enrollments { get; set; }
    }

    public partial class Enrollment
    {
        public string TrackedEntityInstance { get; set; }
        public string Program { get; set; }
        public string Status { get; set; }
        public string OrgUnit { get; set; }
        public DateTimeOffset EnrollmentDate { get; set; }
        public DateTimeOffset IncidentDate { get; set; }
    }
}
