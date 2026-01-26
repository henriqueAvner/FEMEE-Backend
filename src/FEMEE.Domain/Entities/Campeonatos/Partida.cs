using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Campeonatos
{
    public class Partida
    {
        public int Id { get; set; }
        public int CampeonatoId { get; set; }
        public int TimeAId { get; set; }
        public int TimeBId { get; set; }
        public int TimeVencedorId { get; set; }
        public DateTime DataHora { get; set; }
        public string? Local { get; set; }
        public Fase Fase { get; set; }
        public int PlacarTimeA { get; set; }
        public int PlacarTimeB { get; set; }
        public Status Status { get; set; }
        public string? TransmissaoUrl { get; set; }
    }
}