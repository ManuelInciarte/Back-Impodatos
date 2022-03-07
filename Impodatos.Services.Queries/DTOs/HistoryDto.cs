using System;

namespace Impodatos.Services.Queries.DTOs
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public string Programsid { get; set; }
        public string JsonSet { get; set; }
        public string JsonResponse { get; set; }
        public bool State { get; set; }
        public string UserLogin { get; set; }
        public DateTime Fecha { get; set; }
    }
}
