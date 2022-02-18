using MediatR;
using Microsoft.AspNetCore.Http;

namespace Impodatos.Services.EventHandlers.Commands
{
    public class HistoryCreateCommand : INotification
    {
        public string Programsid { get; set; }
        public IFormFile  ExcelFile { get; set; } 
        public string UserLogin { get; set; }  
    }
}
