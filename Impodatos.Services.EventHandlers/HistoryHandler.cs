using Impodatos.Domain;
using Impodatos.Persistence.Database;
using Impodatos.Services.EventHandlers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            await _context.AddAsync(new History
            {                 
                 Programsid = command.Programsid, 
                 JsonSet = command.JsonSet,
                 JsonResponse = command.JsonResponse,
                 State = command.State,
                 UserLogin = command.UserLogin,
                 Fecha = command.Fecha

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
