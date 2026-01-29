using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Campeonatos;

namespace FEMEE.Domain.Interfaces
{
    public interface ICampeonatoRepository : IRepository<Campeonato>
    {
        Task<IEnumerable<Campeonato>> GetCampeonatosAtivosAsync();
        Task<IEnumerable<Campeonato>> GetCampeonatosComInscricoesAbertasAsync();

        Task<IEnumerable<Campeonato>> GetCampeonatosComTimesAsync();

        Task<IEnumerable<Campeonato>> GetCampeonatosByJogoAsync(int jogoId);

        Task<IEnumerable<Campeonato>> GetCampeonatosComPartidasAsync();

        Task<bool> IsTimeInscritoAsync(int campeonatoId, int timeId);

    }
}
