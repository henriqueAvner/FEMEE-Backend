
using FEMEE.Domain.Entities.Campeonatos;
using FEMEE.Domain.Enums;

namespace FEMEE.Domain.Entities.Principal
{
    public class Jogador
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int TimeId { get; set; }

        public string? NickName { get; set; }

        public string? NomeCompleto { get; set; }
        public string? Funcao { get; set; }
        public DateTime DataEntradaTime { get; set; }
        public DateTime DataSaidaTime { get; set; }
        public StatusJogador Status { get; set; }

        public int FuncaoJogador { get; set; }
        public int JogoId { get; set; }

        public User? User { get; set; }
        public Time? Time { get; set; }
    }
}


/*
Na aplicação, ao exibir ou cadastrar, você usa o enum correto conforme o jogo:

Se Jogo.CategoriaJogo == CategoriaJogo.LeagueOfLegends, use FuncaoJogadorLol.
Se Jogo.CategoriaJogo == CategoriaJogo.CounterStrike, use FuncaoJogadorCsgo.
Dessa forma, você mantém a flexibilidade para adicionar mais jogos e suas respectivas funções no futuro.

*/