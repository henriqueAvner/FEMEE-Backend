using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Campeonatos;

namespace FEMEE.Domain.Entities.Principal
{
    public class Conquista
    {
        [Key]
        public int Id { get; set; }
        public int TimeId { get; set; }

        public int CampeonatoId { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Titulo { get; set; }
        [MaxLength(500)]
        public string? Descricao { get; set; }
        public int Posicao { get; set; }
        public DateTime DataConquista { get; set; }
        [MaxLength(256)]
        public string? IconeTitulo { get; set; }

        public Time? Time { get; set; }
        public Campeonato? Campeonato { get; set; }
    }
}