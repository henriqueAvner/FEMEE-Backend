using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace femee_api.Models.Entities.Principal
{
    public class Conquista
    {
        public int Id { get; set; }
        public int Time_Id { get; set; }

        public int Campeonato_Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public int Posicao { get; set; }
        public DateTime Data_Conquista { get; set; }
        public string? Icone_Titulo { get; set; }
    }
}