using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEMEE.Domain.Entities.Principal
{
    public class Noticia
    {
        public int Id { get; set; }

        public int AutorId { get; set; }
        public string? Titulo { get; set; }
        public string? Slug { get; set; }
        public string? Resumo { get; set; }
        public string? Conteudo { get; set; }
        public string? Categoria { get; set; }
        public string? ImagemUrl { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int NumeroComentarios { get; set; }
        public int Vizualizacoes { get; set; }
        public DateTime Publicada { get; set; }

    }
}