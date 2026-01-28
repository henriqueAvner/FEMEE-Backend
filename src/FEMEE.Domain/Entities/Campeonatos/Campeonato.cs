using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Principal;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class Campeonato
    {
        [Key]
        public int Id { get; set; }
        
        public int JogoId { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Titulo { get; set; }
        [MaxLength(1000)]
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        [Required]
        public DateTime DataLimiteInscricao { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Local { get; set; }

        [MaxLength(128)]
        public string? Cidade { get; set; }
        [MaxLength(2)]
        public string? Estado { get; set; }
        public decimal Premiacao { get; set; }
        public int NumeroVagas { get; set; }
        public int NumeroInscritos { get; set; }
        public StatusCampeonato Status { get; set; }
        public Fase FaseCampeonato { get; set; }
        public string? RegulamentoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Jogo? Jogo { get; set; }
        public ICollection<Partida> Partidas { get; set; } = new List<Partida>();
        public ICollection<InscricaoCampeonato> InscricoesCampeonatos { get; set; } = new List<InscricaoCampeonato>();

        public ICollection<Conquista> Conquistas { get; set; } = new List<Conquista>();
    }
}