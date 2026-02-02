using FluentValidation;
using FEMEE.Application.DTOs.Jogador;

namespace FEMEE.Application.Validators.Jogador
{
    /// <summary>
    /// Validador para UpdateJogadorDto.
    /// Valida dados de atualização de jogador.
    /// </summary>
    public class UpdateJogadorDtoValidator : AbstractValidator<UpdateJogadorDto>
    {
        /// <summary>
        /// Construtor que define todas as regras de validação.
        /// </summary>
        public UpdateJogadorDtoValidator()
        {
            // ===== VALIDAR NICKNAME =====
            RuleFor(x => x.NickName)
                .NotEmpty()
                    .WithMessage("Nickname é obrigatório")
                .Length(3, 50)
                    .WithMessage("Nickname deve ter entre 3 e 50 caracteres")
                .Matches(@"^[a-zA-Z0-9_-]+$")
                    .WithMessage("Nickname deve conter apenas letras, números, hífens e underscores");

            // ===== VALIDAR NOME COMPLETO =====
            RuleFor(x => x.NomeCompleto)
                .NotEmpty()
                    .WithMessage("Nome completo é obrigatório")
                .Length(3, 256)
                    .WithMessage("Nome completo deve ter entre 3 e 256 caracteres");

            // ===== VALIDAR FUNÇÃO =====
            RuleFor(x => x.Funcao)
                .IsInEnum()
                    .WithMessage("Função inválida");
        }
    }
}
