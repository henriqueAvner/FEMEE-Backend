using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Campeonatos;

namespace FEMEE.Domain.Interfaces
{
    public interface ICampeonatoService
    {
        Task<Campeonato> CriarCampeonatoAsync(Campeonato campeonato);
        Task<InscricaoCampeonato> InscreverTimeAsync(int campeonatoId, int timeId, 
        int capitaoId);
        Task AprovarInscricaoAsync(int inscricaoId);
        Task RejeitarInscricaoAsync(int inscricaoId, string motivo);
        Task FinalizarCampeonatoAsync(int campeonatoId);
        Task<IEnumerable<Time>> GetRankingAsync(int campeonatoId);
    }
}