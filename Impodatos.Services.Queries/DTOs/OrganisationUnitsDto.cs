using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impodatos.Services.Queries.DTOs
{
    public partial class OrganisationUnitsDto
    {
        public Pager Pager { get; set; }
        public List<OrganisationUnit> OrganisationUnits { get; set; }
    }

    public partial class OrganisationUnit
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
    }

    public partial class Pager
    {
        public long Page { get; set; }
        public long PageCount { get; set; }
        public long Total { get; set; }
        public long PageSize { get; set; }
    }
}
