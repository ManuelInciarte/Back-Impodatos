using Impodatos.Domain;
using Impodatos.Persistence.Database;
using Impodatos.Services.EventHandlers.Commands;
using Impodatos.Services.Queries;
using Impodatos.Services.Queries.DTOs;
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
        private readonly IDhisQueryService _dhis;

        public HistoryHandler(ApplicationDbContext context, IDhisQueryService dhis)
        {
            _context = context;  
            _dhis = dhis;
        }

        public async Task Handle(HistoryCreateCommand command, CancellationToken cancellation)
        {
            //leemos el archivo y guardamos en memoria
            var reader = new StreamReader(command.ExcelFile.OpenReadStream());
            //con el metodo ReadLine leemos la primera linea y guardamos el array en la variable
            var propiedades = reader.ReadLine().Split(';');
            //creamos una variable Object generico
            var listObjResult = new List<Dictionary<string, string>>();
            //recorremos archivo hasta el final
            while (!reader.EndOfStream)
            {
                //leemos cada una de las lineas a partir de la linea dos con el metodo ReadLine, el cual va iterando cada linea
                var valores = reader.ReadLine().Split(';');
                //objeto local generico con dos parametros string
                var objResult = new Dictionary<string, string>();
                //iterando segun la cantidad de propiedades
                for (int j = 0; j < propiedades.Length; j++)
                    //asignamos a cada propiedad el valor
                    objResult.Add(propiedades[j], valores[j]);
                //agregamos cada fila creada en la variable ObjResult
                listObjResult.Add(objResult); 
            }
            //covertimos la lista de objetos genericos creada en un json
            var json = JsonConvert.SerializeObject(listObjResult); 
            //convertimos el archivo a un array de Bytes
            byte[] data = null;
            var fileByte = new BinaryReader(command.ExcelFile.OpenReadStream());
            int i = (int)command.ExcelFile.Length;
            data = fileByte.ReadBytes(i);
            //agregamos al contexto la informacion aguardar
            await _context.AddAsync(new History
            {
                Programsid = command.Programsid,
                JsonSet = json,
                JsonResponse = "No procesado",
                State = false,
                UserLogin = command.UserLogin,
                Fecha = DateTime.Now,
                File = data

            });
            //guardamos
            await _context.SaveChangesAsync();

            ////Llamado de servicios externos ejemplo dhis
            //var trakedResult = await _dhis.AddTracked(new AddTrackedDto()); //crear el objeto de tipo AddTrackedDto
            //var enrollResult = await _dhis.Enrollment(new EnrollmentDto()); //crear el objeto de tipo EnrollmentDto
            //var eventResult = await _dhis.AddEvent(new AddEventDto()); //crear el objeto de tipo AddEventDto

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
