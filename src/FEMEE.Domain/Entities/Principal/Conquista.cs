using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEMEE.Domain.Entities.Principal
{
    public class Conquista
    {
        public int Id { get; set; }
        public int TimeId { get; set; }

        public int CampeonatoId { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public int Posicao { get; set; }
        public DateTime DataConquista { get; set; }
        public string? IconeTitulo { get; set; }
    }
}