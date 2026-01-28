using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEMEE.Domain.Entities.Principal
{
    public class Noticia
    {
        [Key]
        public int Id { get; set; }

        public int AutorId { get; set; }
        [Required]
        [MaxLength(512)]
        public string? Titulo { get; set; }
        [Required]
        [MaxLength(512)]
        public string? Slug { get; set; }
        [MaxLength(1024)]
        public string? Resumo { get; set; }
        public string? Conteudo { get; set; }
        [MaxLength(128)]
        public string? Categoria { get; set; }
        [MaxLength(512)]
        public string? ImagemUrl { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int NumeroComentarios { get; set; }
        public int Vizualizacoes { get; set; }
        public DateTime Publicada { get; set; }

        public User? Autor { get; set; }

    }
}