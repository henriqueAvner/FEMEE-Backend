using System.ComponentModel.DataAnnotations;
using FEMEE.Domain.Entities.Campeonatos;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Principal
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string? Nome { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Email { get; set; }

        [Required]
        public string? Senha { get; set; }
        [MaxLength(20)]
        [Required]
        public string? Telefone { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public ICollection<Noticia>? Noticias { get; set; } = new List<Noticia>();

        public ICollection<InscricaoCampeonato>? InscricoesCampeonatos { get; set; } = new List<InscricaoCampeonato>();
    }
}