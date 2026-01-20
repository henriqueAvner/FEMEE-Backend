using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace femee_api.Models.Entities.Campeonatos
{
    public class Inscricao_Campeonato
    {
        public int Id { get; set; }
        public int Campeonato_Id { get; set; }
        public int Time_Id { get; set; }
        public int Capitao_Id { get; set; }
        public DateTime Data_Inscricao { get; set; }
        public string? Status_Inscricao { get; set; }
        public string? Observacoes { get; set; }
    }
}