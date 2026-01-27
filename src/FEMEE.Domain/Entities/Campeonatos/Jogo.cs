using System.ComponentModel.DataAnnotations;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class Jogo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Slug { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Nome { get; set; }
        [MaxLength(1000)]
        public string? Descricao { get; set; }
        [MaxLength(512)]
        public string? ImagemUrl { get; set; }
        public bool Ativo { get; set; }

        public CategoriaJogo CategoriaJogo { get; set; }

        public ICollection<Campeonato> Campeonatos { get; set; } = new List<Campeonato>();
    }
}