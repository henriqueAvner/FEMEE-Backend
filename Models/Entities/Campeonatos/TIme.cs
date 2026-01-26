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
        public string? LogoUrl { get; set; }
        public DateTime DataFundacao { get; set; }
        public string? Descricao { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Empates { get; set; }
        public int Pontos { get; set; }
        public int PosicaoRanking { get; set; }
        public int PosicaoAnterior { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}