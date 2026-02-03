using FEMEE.Application.DTOs.Campeonato;
using FEMEE.Application.Interfaces.Services;
using FEMEE.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FEMEE.API.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de campeonatos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CampeonatosController : ControllerBase
    {
        private readonly ICampeonatoService _campeonatoService;
        private readonly IValidator<CreateCampeonatoDto> _createValidator;
        private readonly IValidator<UpdateCampeonatoDto> _updateValidator;
        private readonly ILogger<CampeonatosController> _logger;

        public CampeonatosController(
            ICampeonatoService campeonatoService,
            IValidator<CreateCampeonatoDto> createValidator,
            IValidator<UpdateCampeonatoDto> updateValidator,
            ILogger<CampeonatosController> logger)
        {
            _campeonatoService = campeonatoService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        /// <summary>
        /// Obtém um campeonato pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCampeonatoById(int id)
        {
            try
            {
                var campeonato = await _campeonatoService.GetCampeonatoByIdAsync(id);
                return Ok(campeonato);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Obtém todos os campeonatos.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCampeonatos()
        {
            var campeonatos = await _campeonatoService.GetAllCampeonatosAsync();
            return Ok(campeonatos);
        }

        /// <summary>
        /// Obtém campeonatos por status.
        /// </summary>
        [HttpGet("status/{status}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCampeonatosByStatus(StatusCampeonato status)
        {
            var campeonatos = await _campeonatoService.GetCampeonatosByStatusAsync(status);
            return Ok(campeonatos);
        }

        /// <summary>
        /// Cria um novo campeonato.
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateCampeonato([FromBody] CreateCampeonatoDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var campeonato = await _campeonatoService.CreateCampeonatoAsync(dto);
                return CreatedAtAction(nameof(GetCampeonatoById), new { id = campeonato.Id }, campeonato);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um campeonato.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateCampeonato(int id, [FromBody] UpdateCampeonatoDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var campeonato = await _campeonatoService.UpdateCampeonatoAsync(id, dto);
                return Ok(campeonato);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deleta um campeonato.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCampeonato(int id)
        {
            try
            {
                await _campeonatoService.DeleteCampeonatoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
