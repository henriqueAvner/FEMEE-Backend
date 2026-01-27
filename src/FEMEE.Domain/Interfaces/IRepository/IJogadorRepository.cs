using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Principal;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Interfaces
{
    public interface IJogadorRepository : IRepository<Jogador>
    {
        Task<IEnumerable<Jogador>> GetJogadoresByTimeIdAsync(Guid timeId);
        Task<IEnumerable<Jogador>> GetJogadoresAtivosAsync(Guid timeId);

        Task<Jogador> GetByNickNameAsync(string nickName);

        Task<IEnumerable<Jogador>> GetByFuncaoLolAsync(FuncaoJogadorLol funcao);
        Task<IEnumerable<Jogador>> GetByFuncaoCsAsync(FuncaoJogadorCs funcao);

        Task<Jogador> GetWithUserAsync(Guid jogadorId);
        
    }
}