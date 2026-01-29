using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Campeonatos;

namespace FEMEE.Domain.Interfaces
{
    public interface ITimeRepository : IRepository<Time>
    {
        Task<Time> GetBySlugAsync(string slug);
        Task<IEnumerable<Time>> GetRankingAsync(int top = 10);
        Task<IEnumerable<Time>> GetTimesWithPlayersAsync();

        Task<IEnumerable<Time>> GetTimesByCampeonatoIdAsync(int campeonatoId);

        Task AtualizarVitoriasAsync(Guid timeId, int pontos = 3);
        Task AtualizarEmpatesAsync(Guid timeId, int pontos = 1);
        Task AtualizarDerrotasAsync(Guid timeId, int pontos = -3);

        
    }
}
