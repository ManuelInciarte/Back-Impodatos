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
using System.Text.Json;
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
     
            var reader = new StreamReader(command.ExcelFile.OpenReadStream());
            var propiedades = reader.ReadLine().Split(';');
            var listObjResult = new List<Dictionary<string, string>>();
            while (!reader.EndOfStream)
            {
                var valores = reader.ReadLine().Split(';');
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < propiedades.Length; j++)
                    objResult.Add(propiedades[j], valores[j]);

                listObjResult.Add(objResult);
            }
            var json = JsonConvert.SerializeObject(listObjResult);       

            await _context.AddAsync(new History
            {
                Programsid = command.Programsid,
                JsonSet = json,
                JsonResponse = "No procesado",
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
