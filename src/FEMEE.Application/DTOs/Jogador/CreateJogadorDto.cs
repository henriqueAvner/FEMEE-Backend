using FEMEE.Domain.Enums;

namespace FEMEE.Application.DTOs.Jogador
{
    /// <summary>
    /// DTO para criar um novo jogador.
    /// </summary>
    public class CreateJogadorDto
    {
        /// <summary>
        /// ID do time ao qual o jogador pertence.
        /// </summary>
        public int TimeId { get; set; }

        /// <summary>
        /// Nickname do jogador (deve ser único).
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Nome completo do jogador.
        /// </summary>
        public string NomeCompleto { get; set; }

        /// <summary>
        /// Função do jogador no time (Suporte, Carry, Mid, etc).
        /// </summary>
        public FuncaoJogador Funcao { get; set; }
    }
}
