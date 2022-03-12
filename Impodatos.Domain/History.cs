using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impodatos.Domain
{
    public class History
    {
        public int Id { get; set; }
        public string Programsid { get; set; }
        public string JsonSet { get; set; }
        public string JsonResponse { get; set; }
        public bool State { get; set; }
        public string UserLogin { get; set; }
        public DateTime Fecha { get; set; }
        public byte[] File { get; set; }
    }
}
