using System.ComponentModel.DataAnnotations;

namespace WL_Consultings_TestePratico.Models.DTOs.Transacao
{
    public class SaqueDto
    {
        [Required]
        public Guid CarteiraId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "A descrição não pode exceder {1} caracteres.")]
        public string Descricao { get; set; }
    }
}
