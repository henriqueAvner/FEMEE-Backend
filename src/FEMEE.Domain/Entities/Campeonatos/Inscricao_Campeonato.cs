using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class Inscricao_Campeonato
    {
        public int Id { get; set; }
        public int CampeonatoId { get; set; }
        public int TimeId { get; set; }
        public int CapitaoId { get; set; }
        public DateTime DataInscricao { get; set; }
        public string? StatusInscricao { get; set; }
        public string? Observacoes { get; set; }
    }
}