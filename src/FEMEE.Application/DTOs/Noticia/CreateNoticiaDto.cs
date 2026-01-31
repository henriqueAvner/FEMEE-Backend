namespace FEMEE.Application.DTOs.Noticia
{
    /// <summary>
    /// DTO para criar uma nova notícia.
    /// </summary>
    public class CreateNoticiaDto
    {
        /// <summary>
        /// Título da notícia.
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Slug da notícia (URL-friendly).
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Resumo da notícia.
        /// </summary>
        public string Resumo { get; set; }

        /// <summary>
        /// Conteúdo completo da notícia.
        /// </summary>
        public string Conteudo { get; set; }

        /// <summary>
        /// Categoria da notícia.
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// URL da imagem de destaque.
        /// </summary>
        public string ImagemUrl { get; set; }
    }
}
