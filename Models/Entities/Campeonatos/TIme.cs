using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace femee_api.Models.Entities
{
    public class Time
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public string? Slug { get; set; }
        public string? Logo_Url { get; set; }
        public DateTime Data_Fundacao { get; set; }
        public string? Descricao { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Empates { get; set; }
        public int Pontos { get; set; }
        public int Posicao_ranking { get; set; }
        public int Posicao_anterior { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}