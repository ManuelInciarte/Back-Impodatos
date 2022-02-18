using Microsoft.EntityFrameworkCore;
using Impodatos.Persistence.Database;
using Impodatos.Services.Queries.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Impodatos.Domain;
using AutoMapper;
using System.Linq;

namespace Impodatos.Services.Queries
{
    public interface IHistoryQueryService 
    {
        Task<IEnumerable<HistoryDto>> GetAllAsync();
        Task<IEnumerable<HistoryDto>> GetHistoryUserAsync(string correo);
    }
    public  class HistoryQueryService : IHistoryQueryService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;
        public HistoryQueryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        _mapper = mapper;
        }

        public async Task<IEnumerable<HistoryDto>> GetAllAsync()
        {
            var result = await _context.History.ToListAsync();

            return _mapper.Map<IEnumerable<HistoryDto>>(result);
        }
        public async Task<IEnumerable<HistoryDto>> GetHistoryUserAsync(string correo)
        {
            var result = await (from c in _context.History
                                where c.UserLogin == correo
                                select new History
                                {
                                    Id = c.Id,
                                    Programsid = c.Programsid,
                                    JsonSet = c.JsonSet,
                                    JsonResponse = c.JsonResponse,
                                    State = c.State,
                                    UserLogin = c.UserLogin,
                                    Fecha = c.Fecha

                                }).ToListAsync();

            return _mapper.Map<IEnumerable<HistoryDto>>(result);
        }


    }
}
