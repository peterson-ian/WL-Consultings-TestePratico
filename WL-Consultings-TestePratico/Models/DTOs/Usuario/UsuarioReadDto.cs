using WL_Consultings_TestePratico.Models.DTOs.Carteira;

namespace WL_Consultings_TestePratico.Models.DTOs.Usuario
{
    public class UsuarioReadDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }

        public bool Bloqueado { get; set; }

        public bool Ativo { get; set; }

        public int TentativasLogin { get; set; } = 0;
        public Guid CarteiraId { get; set; }
    }
}
