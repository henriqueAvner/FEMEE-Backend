using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Principal;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class Campeonato
    {
        public int Id { get; set; }
        
        public int JogoId { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DataLimiteInscricao { get; set; }
        public string? Local { get; set; }

        public string? Cidade { get; set; }
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