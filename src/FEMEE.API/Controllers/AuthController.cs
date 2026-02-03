using FEMEE.Application.DTOs.Auth;
using FEMEE.Application.Interfaces.Common;
using FEMEE.Application.Interfaces.Services;
using FEMEE.Application.Services.Auth;
using FEMEE.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FEMEE.API.Controllers
{
    /// <summary>
    /// Controller para autenticação.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthenticationService authService,
            IUserService userService,
            IValidator<LoginRequest> loginValidator,
            IValidator<RegisterRequest> registerValidator,
            IPasswordHasher passwordHasher,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _userService = userService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        /// <summary>
        /// Faz login de um usuário.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var validationResult = await _loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var user = await _userService.GetUserByEmailAsync(request.Email);

                if (!_passwordHasher.VerifyPassword(request.Senha, user.SenhaHash))
                    return Unauthorized("Email ou senha inválidos");

                var token = await _authService.GenerateTokenAsync(request.Email);

                return Ok(new LoginResponse
                {
                    Token = token,
                    UserId = user.Id,
                    Email = user.Email,
                    Nome = user.Nome
                });
            }
            catch (KeyNotFoundException)
            {
                return Unauthorized("Email ou senha inválidos");
            }
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var validationResult = await _registerValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var createUserDto = new FEMEE.Application.DTOs.User.CreateUserDto
                {
                    Nome = request.Nome,
                    Email = request.Email,
                    Senha = request.Senha,
                    Telefone = request.Telefone,
                    TipoUsuario = FEMEE.Domain.Enums.TipoUsuario.Jogador
                };

                var user = await _userService.CreateUserAsync(createUserDto);
                return CreatedAtAction(nameof(GetCurrentUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém o usuário atual autenticado.
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized();

            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }
    }
}
