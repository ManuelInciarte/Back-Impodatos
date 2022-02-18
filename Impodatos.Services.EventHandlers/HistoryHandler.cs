using Impodatos.Domain;
using Impodatos.Persistence.Database;
using Impodatos.Services.EventHandlers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Impodatos.Services.EventHandlers
{    
    public class HistoryHandler : 
        INotificationHandler<HistoryCreateCommand>,
        INotificationHandler<HistoryUpdateCommand>
    {
        private readonly ApplicationDbContext _context;

        public HistoryHandler(ApplicationDbContext context)
        {
            _context = context;          
        }

        public async Task Handle(HistoryCreateCommand command, CancellationToken cancellation)
        {

            MemoryStream ms = new MemoryStream();
            await command.ExcelFile.CopyToAsync(ms);
            var content = Encoding.UTF8.GetString(ms.ToArray());
            string[] datos = content.Split("\r\n");       
            var properties = datos[0].Split(";");








            var valor = datos[1].Split(";");
            dynamic Objeto = new ExpandoObject();

            var objResult = new Dictionary<string, string>();
            objResult.Add(properties[0], valor[0]);
            objResult.Add(properties[1], valor[69]);




            var result2 = JsonConvert.SerializeObject(objResult);
         
     
                        SLDocument documento = new SLDocument(ms);
            var columnas = documento.GetWorksheetStatistics().NumberOfColumns;
            var filas = documento.GetWorksheetStatistics().NumberOfRows;
       



            await _context.AddAsync(new History
            {                 
                 Programsid = command.Programsid, 
                 //JsonSet = command.JsonSet,
                 //JsonResponse = command.JsonResponse,
                 State = false,
                 UserLogin = command.UserLogin,
                 Fecha = DateTime.Now

            });
            await _context.SaveChangesAsync();
        }

        public async Task Handle(HistoryUpdateCommand command, CancellationToken cancellation)
        {
            var History = await _context.History.FindAsync(command.Id);
            History.JsonResponse = command.JsonResponse;
            History.State = command.State;

            await _context.SaveChangesAsync();

        }
    }
}
