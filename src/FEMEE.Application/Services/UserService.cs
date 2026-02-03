using AutoMapper;
using FEMEE.Application.DTOs.User;
using FEMEE.Domain.Entities.Principal;
using FEMEE.Application.Interfaces.Repositories;
using FEMEE.Application.Interfaces.Services;
using FEMEE.Application.Interfaces.Common;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FEMEE.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<UserService> logger,
            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
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
            using (StructuredLogging.BeginOperationScope("CreateUser"))
            {
                StructuredLogging.AddEntityContext("User", 0);

                var stopwatch = Stopwatch.StartNew();

                try
                {
                    _logger.LogInformation("Iniciando criação de usuário: {Email}", dto.Email);

                    var users = await _unitOfWork.Users.GetAllAsync();
                    if (users.Any(u => u.Email == dto.Email))
                    {
                        _logger.LogWarning("Email já cadastrado: {Email}", dto.Email);
                        throw new InvalidOperationException("Email já cadastrado");
                    }

                    var user = _mapper.Map<User>(dto);
                    user.Senha = _passwordHasher.HashPassword(dto.Senha);
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
}
