using FEMEE.Domain.Entities.Campeonatos;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Principal
{
    public class User
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public ICollection<Jogador>? Jogadores { get; set; } = new List<Jogador>();
        public ICollection<Noticia>? Noticias { get; set; } = new List<Noticia>();

        public ICollection<InscricaoCampeonato>? InscricoesCampeonatos { get; set; } = new List<InscricaoCampeonato>();
    }
}