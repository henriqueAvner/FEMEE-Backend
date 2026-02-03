using FEMEE.Application.DTOs.Campeonato;
using FEMEE.Domain.Enums;

namespace FEMEE.Application.Interfaces.Services
{
    /// <summary>
    /// Interface para o serviço de campeonatos.
    /// </summary>
    public interface ICampeonatoService
    {
        Task<CampeonatoResponseDto> GetCampeonatoByIdAsync(int id);
        Task<IEnumerable<CampeonatoResponseDto>> GetAllCampeonatosAsync();
        Task<IEnumerable<CampeonatoResponseDto>> GetCampeonatosByStatusAsync(StatusCampeonato status);
        Task<CampeonatoResponseDto> CreateCampeonatoAsync(CreateCampeonatoDto dto);
        Task<CampeonatoResponseDto> UpdateCampeonatoAsync(int id, UpdateCampeonatoDto dto);
        Task DeleteCampeonatoAsync(int id);
    }
}
