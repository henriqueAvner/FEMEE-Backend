
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FEMEE.Domain.Entities.Campeonatos;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Principal
{
    public class Jogador : User
    {
        [Required]
        [MaxLength(256)]
        [Column("NICKNAME")]
        public string? NickName { get; set; }

        [MaxLength(512)]
        [Column("FOTO_URL")]
        public string? FotoUrl { get; set; }

        [Required]
        [Column("FUNCAO")]
        //Conversao no DTO para as modalidades de jogo
        public FuncaoJogador Funcao { get; set; }
        [Column("DATA_ENTRADA_TIME")]
        public DateTime DataEntradaTime { get; set; }
        [Column("DATA_SAIDA_TIME")]
        public DateTime DataSaidaTime { get; set; }
        [Column("STATUS")]
        public StatusJogador Status { get; set; }
        public Time? Time { get; set; }
        [Column("TIME_ID")]
        public int? TimeId {get; set;}
    }
}


/*
Na aplicação, ao exibir ou cadastrar, você usa o enum correto conforme o jogo:

Se Jogo.CategoriaJogo == CategoriaJogo.LeagueOfLegends, use FuncaoJogadorLol.
Se Jogo.CategoriaJogo == CategoriaJogo.CounterStrike, use FuncaoJogadorCsgo.
Dessa forma, você mantém a flexibilidade para adicionar mais jogos e suas respectivas funções no futuro.

*/