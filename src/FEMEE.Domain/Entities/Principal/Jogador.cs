
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
        public Status Status { get; set; }
    }
}