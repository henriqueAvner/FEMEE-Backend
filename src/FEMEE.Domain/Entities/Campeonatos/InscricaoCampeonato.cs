using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Principal;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class InscricaoCampeonato
    {
        public int Id { get; set; }
        public int CampeonatoId { get; set; }
        public int TimeId { get; set; }
        public int CapitaoId { get; set; }
        public DateTime DataInscricao { get; set; }
        public bool StatusInscricao { get; set; }
        public string? Observacoes { get; set; }

        public Campeonato? Campeonato { get; set; }
        public Time? Time { get; set; }
        public User? Capitao { get; set; }
    }
}