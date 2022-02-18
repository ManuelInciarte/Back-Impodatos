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
            string[] datos = content.Split("\n");
            var Obj = datos[0].Split(";");
            dynamic Objeto = new ExpandoObject();

            string propiedad = Obj[0];
            Objeto["Hola"] = "Manuel";

            string[] arr_sample = new string[Obj.Length];
            for (int i=0; i< Obj.Length; i++)
            {
                arr_sample[i] = Obj[i];
            }

           
            var result = arr_sample;
            var result2 = JsonConvert.SerializeObject(Objeto);
     




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
