// FEMEE.Application/Services/UserService.cs

using AutoMapper;
using FEMEE.API.Configuration;
using FEMEE.Application.DTOs.User;
using FEMEE.Domain.Entities.Principal;
using FEMEE.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UserResponseDto> GetUserByIdAsync(int id)
    {
        // ===== USAR ESCOPO DE LOGGING =====
        using (StructuredLogging.BeginOperationScope("GetUserById", id))
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation("Buscando usuário com ID: {UserId}", id);

                var user = await _unitOfWork.Users.GetByIdAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("Usuário não encontrado: {UserId}", id);
                    throw new KeyNotFoundException($"Usuário com ID {id} não encontrado");
                }

                stopwatch.Stop();
                _logger.LogInformation("Usuário encontrado em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);

                return _mapper.Map<UserResponseDto>(user);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Erro ao buscar usuário em {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
    {
        // ===== USAR ESCOPO DE LOGGING =====
        using (StructuredLogging.BeginOperationScope("CreateUser"))
        {
            StructuredLogging.AddEntityContext("User", 0);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation("Iniciando criação de usuário: {Email}", dto.Email);

                // Verificar se email já existe
                var users = await _unitOfWork.Users.GetAllAsync();
                if (users.Any(u => u.Email == dto.Email))
                {
                    _logger.LogWarning("Email já cadastrado: {Email}", dto.Email);
                    throw new InvalidOperationException("Email já cadastrado");
                }

                var user = _mapper.Map<User>(dto);
                user.SenhaHash = PasswordHasher.HashPassword(dto.Senha);
                user.DataCriacao = DateTime.UtcNow;
                user.DataAtualizacao = DateTime.UtcNow;

                _logger.LogDebug("Usuário mapeado e senha hasheada");

                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                stopwatch.Stop();
                _logger.LogInformation("Usuário criado com sucesso em {ElapsedMilliseconds}ms: {UserId}",
                    stopwatch.ElapsedMilliseconds, user.Id);

                return _mapper.Map<UserResponseDto>(user);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Erro ao criar usuário em {ElapsedMilliseconds}ms: {Email}",
                    stopwatch.ElapsedMilliseconds, dto.Email);
                throw;
            }
        }
    }
}
