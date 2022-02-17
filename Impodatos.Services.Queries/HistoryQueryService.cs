using Microsoft.EntityFrameworkCore;
using Impodatos.Persistence.Database;
using Impodatos.Services.Queries.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Impodatos.Domain;

namespace Impodatos.Services.Queries
{
    public interface IHistoryQueryService
    {
        Task<IEnumerable<History>> GetAllAsync();
    }
    public  class HistoryQueryService : IHistoryQueryService
    {
        private readonly ApplicationDbContext _context;
        public HistoryQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<History>> GetAllAsync()
        {
            var result = await _context.History.ToListAsync();
            return result;
        }

    }
}
