using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Campeonatos;

namespace FEMEE.Application.Interfaces.Services
{
    public interface IPartidaService
    {
        Task<Partida> CriarPartidaAsync(Partida partida);
        Task FinalizarPartidaAsync(int partidaId, int? timeVencedorId, int placarA,
        int placarB);
        Task<IEnumerable<Partida>> GetPartidasByCampeonatoAsync(int campeonatoId);
        Task<IEnumerable<Partida>> GetHistoricoAsync(int timeAId, int timeBId);
    }
}