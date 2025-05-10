using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WL_Consultings_TestePratico.Models.Entities
{
    [Table("transacao")]
    public class Transacao
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string CodigoTransacao { get; set; }

        public Guid? CarteiraOrigemId { get; set; }

        [Required]
        public Guid CarteiraDestinoId { get; set; }

        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataProcessamento { get; set; }

        public DateTime? DataConclusao { get; set; }

        public string Descricao { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        public string Tipo { get; set; }

        public string MotivoFalha { get; set; }

        [Required]
        [StringLength(128)]
        public string HashVerificacao { get; set; }
        public virtual Carteira? CarteiraOrigem { get; set; }
        public virtual Carteira CarteiraDestino { get; set; }
    }
}
