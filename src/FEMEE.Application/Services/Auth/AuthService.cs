using FEMEE.Application.DTOs.Auth;
using FEMEE.Domain.Entities.Principal;
using FEMEE.Domain.Interfaces;

namespace FEMEE.Application.Services.Auth
{
    public class AuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IUnitOfWork unitOfWork,
            IAuthenticationService authenticationService,
            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        // ===== MÉTODOS PÚBLICOS =====

        /// <summary>
        /// Realiza login do usuário.
        /// Valida email e senha, gera token JWT.
        /// </summary>
        /// <param name="request">Dados de login</param>
        /// <returns>Resposta com token e dados do usuário</returns>
        /// <exception cref="UnauthorizedAccessException">Se email ou senha inválidos</exception>
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));
            if(string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
                    throw new ArgumentException("Email e senha são obrigatórios.");

            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();
                var user = users.FirstOrDefault(u => u.Email == request.Email);

                if (user == null)
                    throw new UnauthorizedAccessException("Email ou senha inválidos.");

                // Verificar senha (User.Senha guarda o hash BCrypt)
                if (!_passwordHasher.VerifyPassword(request.Senha, user.Senha!))
                    throw new UnauthorizedAccessException("Email ou senha inválidos.");

                var token = await _authenticationService.GenerateTokenAsync(user);
                var expiresAt = DateTime.UtcNow.AddMinutes(60);

                return new LoginResponse
                {
                    Token = token,
                    UserId = user.Id,
                    Email = user.Email!,
                    Nome = user.Nome!,
                    TipoUsuario = user.TipoUsuario,
                    ExpiresAt = expiresAt // ou obter de JwtSettings se necessário
                };
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Logar exceção se necessário
                throw new InvalidOperationException("Erro ao processar login.", ex);
            }
        }

        /// <summary>
        /// Registra um novo usuário.
        /// Valida dados, faz hash da senha, cria usuário.
        /// </summary>
        /// <param name="request">Dados de registro</param>
        /// <returns>Resposta com token e dados do novo usuário</returns>
        /// <exception cref="InvalidOperationException">Se email já existe ou dados inválidos</exception>
        public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            //Validar os dados:
            if (string.IsNullOrWhiteSpace(request.Nome))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email é obrigatório");

            if (string.IsNullOrWhiteSpace(request.Senha))
                throw new ArgumentException("Senha é obrigatória");

            if (request.Senha != request.ConfirmacaoSenha)
                throw new ArgumentException("Senhas não correspondem");

            if (request.Senha.Length < 8)
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres");
            try
            {
                // Verificar se email já existe
                var users = await _unitOfWork.Users.GetAllAsync();
                if (users.Any(u => u.Email == request.Email))
                    throw new InvalidOperationException("Email já cadastrado");

                // Criar novo usuário
                var user = new User
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    Telefone = request.Telefone,
                    TipoUsuario = request.TipoUsuario,
                    Senha = _passwordHasher.HashPassword(request.Senha),
                    DataCriacao = DateTime.UtcNow,   
                };

                // Adicionar ao banco
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                // Gerar token
                var token = await _authenticationService.GenerateTokenAsync(user);

                var expiresAt = DateTime.UtcNow.AddMinutes(60);

                return new LoginResponse
                {
                    Token = token,
                    UserId = user.Id,
                    Email = user.Email,
                    Nome = user.Nome,
                    TipoUsuario = user.TipoUsuario,
                    ExpiresAt = expiresAt
                };
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao registrar usuário", ex);
            }
        }

        /// <summary>
        /// Atualiza a senha de um usuário.
        /// Valida senha atual antes de atualizar.
        /// </summary>
        /// <param name="userId">ID do usuário</param>
        /// <param name="senhaAtual">Senha atual em texto plano</param>
        /// <param name="novaSenha">Nova senha em texto plano</param>
        public async Task ChangePasswordAsync(int userId, string senhaAtual, string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(senhaAtual))
                throw new ArgumentException("Senha atual é obrigatória");

            if (string.IsNullOrWhiteSpace(novaSenha))
                throw new ArgumentException("Nova senha é obrigatória");

            if (novaSenha.Length < 8)
                throw new ArgumentException("Nova senha deve ter pelo menos 8 caracteres");

            try
            {
                // Buscar usuário
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    throw new InvalidOperationException("Usuário não encontrado");

                // Verificar senha atual
                if (!_passwordHasher.VerifyPassword(senhaAtual, user.Senha))
                    throw new UnauthorizedAccessException("Senha atual inválida");

                // Atualizar senha
                user.Senha = _passwordHasher.HashPassword(novaSenha);
                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
