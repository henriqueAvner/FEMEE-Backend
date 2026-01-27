
using System.ComponentModel.DataAnnotations;
using FEMEE.Domain.Entities.Campeonatos;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Principal
{
    public class Jogador : User
    {
        [Required]
        [MaxLength(256)]
        public string? NickName { get; set; }

        [Required]
        public FuncaoJogador Funcao { get; set; }
        public DateTime DataEntradaTime { get; set; }
        public DateTime DataSaidaTime { get; set; }

        public StatusJogador Status { get; set; }

        public int FuncaoJogador { get; set; }
        public Time? Time { get; set; }
        public int TimeId {get; set;}
    }
}


/*
Na aplicação, ao exibir ou cadastrar, você usa o enum correto conforme o jogo:

Se Jogo.CategoriaJogo == CategoriaJogo.LeagueOfLegends, use FuncaoJogadorLol.
Se Jogo.CategoriaJogo == CategoriaJogo.CounterStrike, use FuncaoJogadorCsgo.
Dessa forma, você mantém a flexibilidade para adicionar mais jogos e suas respectivas funções no futuro.

*/