using System.Collections.Generic;

namespace Impodatos.Services.Queries.DTOs
{

    public partial class DhisProgramDto
    {
        public List<object> Datasets { get; set; }
        public List<Program> Programs { get; set; }
    }

    public partial class Program
    {
        public string Status { get; set; }
        public string Programid { get; set; }
        public string Programname { get; set; }
        public List<Attributemapping> Attributemapping { get; set; }
    }

    public partial class Attributemapping
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Column { get; set; }
    }
}
