using System.ComponentModel.DataAnnotations;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class Partida
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CampeonatoId { get; set; }
        [Required]
        public int TimeAId { get; set; }
        [Required]
        public int TimeBId { get; set; }
        [Required]
        public int TimeVencedorId { get; set; }
        [Required]
        public DateTime DataHora { get; set; }
        public string? Local { get; set; }
        [Required]
        public Fase Fase { get; set; }
        public int PlacarTimeA { get; set; }
        public int PlacarTimeB { get; set; }
        public StatusPartida Status { get; set; }
        [MaxLength(512)]
        public string? TransmissaoUrl { get; set; }
        [Required]
        public Campeonato? Campeonato { get; set; }
        [Required]
        public Time? TimeA { get; set; }
        [Required]
        public Time? TimeB { get; set; }
        [Required]
        public Time? TimeVencedor { get; set; }
    }
}