
using femee_api.Models.Enums;

namespace femee_api.Models.Entities.Principal
{
    public class Jogador
    {
        public int Id { get; set; }

        public int User_Id { get; set; }
        public int Time_Id { get; set; }

        public string? NickName { get; set; }

        public string? Nome_Completo { get; set; }
        public string? Funcao { get; set; }
        public DateTime Data_Entrada_Time { get; set; }
        public DateTime Data_Saida_Time { get; set; }
        public Status Status { get; set; }
    }
}