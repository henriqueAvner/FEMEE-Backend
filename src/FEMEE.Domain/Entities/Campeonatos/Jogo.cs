using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class Jogo
    {
        public int Id { get; set; }
        public string? Slug { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? ImagemUrl { get; set; }
        public bool Ativo { get; set; }

        public CategoriaJogo CategoriaJogo { get; set; }

        public ICollection<Campeonato> Campeonatos { get; set; } = new List<Campeonato>();
    }
}