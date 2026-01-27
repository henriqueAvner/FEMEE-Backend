using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEMEE.Domain.Interfaces.IService
{
    public interface ICacheService
    {
        Task<T> GetTAsync<T>(string chave);

        Task SetAsync<T>(string chave, T valor, TimeSpan? expiracao = null);
        Task RemoveAsync(string chave);
        Task ClearAsync();

        Task<bool> ExistsAsync(string chave);
    }
}