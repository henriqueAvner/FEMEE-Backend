using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Campeonatos;
using FEMEE.Domain.Interfaces;
using FEMEE.Infrastructure.Data.Context;

namespace FEMEE.Infrastructure.Data.Repositories
{
    public class TimeRepository : GenericRepository<Time>, ITimeRepository
    {
        private readonly FemeeDbContext _context;
        public TimeRepository(FemeeDbContext context) : base(context)
        {
            _context = context;
            
        }

        public Task AtualizarDerrotasAsync(Guid timeId, int pontos = -3)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarEmpatesAsync(Guid timeId, int pontos = 1)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarVitoriasAsync(Guid timeId, int pontos = 3)
        {
            throw new NotImplementedException();
        }

        public Task<Time> GetBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Time>> GetRankingAsync(int top = 10)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Time>> GetTimesByCampeonatoIdAsync(Guid campeonatoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Time>> GetTimesWithPlayersAsync()
        {
            throw new NotImplementedException();
        }
    }
}