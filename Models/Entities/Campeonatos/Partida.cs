using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using femee_api.Models.Enums;

namespace femee_api.Models.Entities.Campeonatos
{
    public class Partida
    {
        public int Id { get; set; }
        public int Campeonato_Id { get; set; }
        public int Time_A_Id { get; set; }
        public int Time_B_Id { get; set; }
        public int Time_Vencedor_Id { get; set; }
        public DateTime Data_Hora { get; set; }
        public string? Local { get; set; }
        public Fase Fase { get; set; }
        public int Placar_Time_A { get; set; }
        public int Placar_Time_B { get; set; }
        public Status Status { get; set; }
        public string? Transmissao_Url { get; set; }
    }
}