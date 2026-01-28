using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FEMEE.Domain.Entities.Principal
{
    public class Noticia
    {
        [Key]
        public int Id { get; set; }

        [Column("AUTOR_ID")]
        public int AutorId { get; set; }
        [Required]
        [MaxLength(512)]
        [Column("TITULO")]
        public string? Titulo { get; set; }
        [Required]
        [MaxLength(512)]
        [Column("SLUG")]
        public string? Slug { get; set; }
        [MaxLength(1024)]
        [Column("RESUMO")]
        public string? Resumo { get; set; }

        [Column("CONTEUDO")]
        public string? Conteudo { get; set; }
        [MaxLength(128)]
        [Column("CATEGORIA")]
        public string? Categoria { get; set; }
        [MaxLength(512)]
        [Column("IMAGEM_URL")]
        public string? ImagemUrl { get; set; }
        [Column("DATA_PUBLICACAO")]
        public DateTime DataPublicacao { get; set; }
        [Column("NUMERO_COMENTARIOS")]
        public int NumeroComentarios { get; set; }
        [Column("VISUALIZACOES")]
        public int Vizualizacoes { get; set; }
        [Column("PUBLICADA")]
        public DateTime Publicada { get; set; }

        public User? Autor { get; set; }

    }
}