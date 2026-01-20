using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace femee_api.Models.Entities
{
    public class Jogo
    {
        public int Id { get; set; }
        public string? Slug { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? ImagemUrl { get; set; }
        public bool Ativo { get; set; }
    }
}