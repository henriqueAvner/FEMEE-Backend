using FEMEE.Domain.Enums;

namespace FEMEE.Application.DTOs.Jogador
{
    /// <summary>
    /// DTO para atualizar um jogador existente.
    /// </summary>
    public class UpdateJogadorDto
    {
        /// <summary>
        /// Novo nickname.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Novo nome completo.
        /// </summary>
        public string NomeCompleto { get; set; }

        /// <summary>
        /// Nova função.
        /// </summary>
        public FuncaoJogador Funcao { get; set; }
    }
}
