using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WL_Consultings_TestePratico.Models.Entities
{
    [Table("carteira")]
    public class Carteira
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UsuarioId { get; set; }

        [Required]
        [StringLength(10)]
        public string Moeda { get; set; } = "BRL";

        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Saldo { get; set; } = 0.0000m;

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Transacao> TransferenciasOrigem { get; set; }
        public virtual ICollection<Transacao> TransferenciasDestino { get; set; }
    }
}
