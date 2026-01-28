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
    public class InscricaoCampeonato
    {
        [Key]
        public int Id { get; set; }
        [Column("CAMPEONATO_ID")]
        public int CampeonatoId { get; set; }
        [Column("TIME_ID")]
        public int TimeId { get; set; }
        [Column("CAPITAO_ID")]
        public int CapitaoId { get; set; }
        [Column("DATA_INSCRICAO")]
        public DateTime DataInscricao { get; set; }
        [Column("STATUS_INSCRICAO")]
        public StatusInscricao StatusInscricao { get; set; }
        [MaxLength(500)]
        [Column("OBSERVACOES")]
        public string? Observacoes { get; set; }

        public Campeonato? Campeonato { get; set; }
        public Time? Time { get; set; }
        public User? Capitao { get; set; }
    }
}