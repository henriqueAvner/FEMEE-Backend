namespace FEMEE.Application.DTOs.Produto
{
    /// <summary>
    /// DTO para atualizar um produto existente.
    /// </summary>
    public class UpdateProdutoDto
    {
        /// <summary>
        /// Novo nome.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Nova descrição.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Novo preço.
        /// </summary>
        public decimal Preco { get; set; }

        /// <summary>
        /// Nova imagem.
        /// </summary>
        public string ImagemUrl { get; set; }

        /// <summary>
        /// Novo estoque.
        /// </summary>
        public int Estoque { get; set; }
    }
}
