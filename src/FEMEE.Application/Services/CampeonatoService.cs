using AutoMapper;
using FEMEE.Application.DTOs.Campeonato;
using FEMEE.Application.Interfaces.Repositories;
using FEMEE.Application.Interfaces.Services;
using FEMEE.Application.Logging;
using FEMEE.Domain.Entities.Campeonatos;
using FEMEE.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FEMEE.Application.Services
{
    /// <summary>
    /// Serviço de campeonatos.
    /// </summary>
    public class CampeonatoService : ICampeonatoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CampeonatoService> _logger;

        public CampeonatoService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CampeonatoService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CampeonatoResponseDto> GetCampeonatoByIdAsync(int id)
        {
            using (StructuredLogging.BeginOperationScope("GetCampeonatoById", id))
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("Buscando campeonato com ID: {CampeonatoId}", id);
                    var campeonato = await _unitOfWork.Campeonatos.GetByIdAsync(id);

                    if (campeonato == null)
                    {
                        _logger.LogWarning("Campeonato não encontrado: {CampeonatoId}", id);
                        throw new KeyNotFoundException($"Campeonato com ID {id} não encontrado");
                    }

                    stopwatch.Stop();
                    _logger.LogInformation("Campeonato encontrado em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                    return _mapper.Map<CampeonatoResponseDto>(campeonato);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _logger.LogError(ex, "Erro ao buscar campeonato em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }

        public async Task<IEnumerable<CampeonatoResponseDto>> GetAllCampeonatosAsync()
        {
            using (StructuredLogging.BeginOperationScope("GetAllCampeonatos"))
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("Buscando todos os campeonatos");
                    var campeonatos = await _unitOfWork.Campeonatos.GetAllAsync();

                    stopwatch.Stop();
                    _logger.LogInformation("Total de campeonatos encontrados: {Count} em {ElapsedMilliseconds}ms",
                        campeonatos.Count(), stopwatch.ElapsedMilliseconds);

                    return _mapper.Map<IEnumerable<CampeonatoResponseDto>>(campeonatos);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _logger.LogError(ex, "Erro ao buscar todos os campeonatos em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }

        public async Task<IEnumerable<CampeonatoResponseDto>> GetCampeonatosByStatusAsync(StatusCampeonato status)
        {
            using (StructuredLogging.BeginOperationScope("GetCampeonatosByStatus"))
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("Buscando campeonatos com status: {Status}", status);
                    var campeonatos = await _unitOfWork.Campeonatos.GetAllAsync();
                    var filtered = campeonatos.Where(c => c.Status == status).ToList();

                    stopwatch.Stop();
                    _logger.LogInformation("Total de campeonatos encontrados: {Count} em {ElapsedMilliseconds}ms",
                        filtered.Count, stopwatch.ElapsedMilliseconds);

                    return _mapper.Map<IEnumerable<CampeonatoResponseDto>>(filtered);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _logger.LogError(ex, "Erro ao buscar campeonatos por status em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }

        public async Task<CampeonatoResponseDto> CreateCampeonatoAsync(CreateCampeonatoDto dto)
        {
            using (StructuredLogging.BeginOperationScope("CreateCampeonato"))
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("Criando novo campeonato: {Titulo}", dto.Titulo);

                    var campeonato = _mapper.Map<Campeonato>(dto);
                    campeonato.CreatedAt = DateTime.UtcNow;
                    campeonato.DataInicio = DateTime.UtcNow;
                    campeonato.Status = StatusCampeonato.Open;

                    await _unitOfWork.Campeonatos.AddAsync(campeonato);
                    await _unitOfWork.SaveChangesAsync();

                    stopwatch.Stop();
                    _logger.LogInformation("Campeonato criado com sucesso em {ElapsedMilliseconds}ms: {CampeonatoId}",
                        stopwatch.ElapsedMilliseconds, campeonato.Id);

                    return _mapper.Map<CampeonatoResponseDto>(campeonato);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _logger.LogError(ex, "Erro ao criar campeonato em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }

        public async Task<CampeonatoResponseDto> UpdateCampeonatoAsync(int id, UpdateCampeonatoDto dto)
        {
            using (StructuredLogging.BeginOperationScope("UpdateCampeonato", id))
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("Atualizando campeonato: {CampeonatoId}", id);

                    var campeonato = await _unitOfWork.Campeonatos.GetByIdAsync(id);
                    if (campeonato == null)
                    {
                        _logger.LogWarning("Campeonato não encontrado: {CampeonatoId}", id);
                        throw new KeyNotFoundException($"Campeonato com ID {id} não encontrado");
                    }

                    _mapper.Map(dto, campeonato);
                    campeonato.UpdatedAt = DateTime.UtcNow;

                    await _unitOfWork.Campeonatos.UpdateAsync(campeonato);
                    await _unitOfWork.SaveChangesAsync();

                    stopwatch.Stop();
                    _logger.LogInformation("Campeonato atualizado com sucesso em {ElapsedMilliseconds}ms",
                        stopwatch.ElapsedMilliseconds);

                    return _mapper.Map<CampeonatoResponseDto>(campeonato);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _logger.LogError(ex, "Erro ao atualizar campeonato em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }

        public async Task DeleteCampeonatoAsync(int id)
        {
            using (StructuredLogging.BeginOperationScope("DeleteCampeonato", id))
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("Deletando campeonato: {CampeonatoId}", id);

                    var campeonato = await _unitOfWork.Campeonatos.GetByIdAsync(id);
                    if (campeonato == null)
                    {
                        _logger.LogWarning("Campeonato não encontrado: {CampeonatoId}", id);
                        throw new KeyNotFoundException($"Campeonato com ID {id} não encontrado");
                    }

                    await _unitOfWork.Campeonatos.DeleteAsync(id);
                    await _unitOfWork.SaveChangesAsync();

                    stopwatch.Stop();
                    _logger.LogInformation("Campeonato deletado com sucesso em {ElapsedMilliseconds}ms",
                        stopwatch.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _logger.LogError(ex, "Erro ao deletar campeonato em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                    throw;
                }
            }
        }
    }
}
