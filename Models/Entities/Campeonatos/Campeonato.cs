using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace femee_api.Models.Entities
{
    public class Campeonato
    {
        public int Id { get; set; }
        
        public int Jogo_Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Fim { get; set; }
        public DateTime Data_Limite_Inscricao { get; set; }
        public string? Local { get; set; }

        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public decimal Premiacao { get; set; }
        public int Numero_Vagas { get; set; }
        public int Numero_Inscritos { get; set; }
        public Enums.Status Status { get; set; }
        public string? Regulamento_Url { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}