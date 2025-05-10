using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WL_Consultings_TestePratico.Models.Entities
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nome { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }

        public bool Bloqueado { get; set; } = false;

        public bool Ativo { get; set; } = true;

        public int TentativasLogin { get; set; } = 0;
        public virtual Carteira Carteira { get; set; }
    }

}
