namespace FEMEE.Application.DTOs.Time
{
    /// <summary>
    /// DTO para criar um novo time.
    /// </summary>
    public class CreateTimeDto
    {
        /// <summary>
        /// Nome do time.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Slug do time (URL-friendly).
        /// Exemplo: "thunder-gaming"
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// URL do logo do time.
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Data de fundação do time.
        /// </summary>
        public DateTime DataFundacao { get; set; }

        /// <summary>
        /// Descrição do time.
        /// </summary>
        public string Descricao { get; set; }
    }
}
