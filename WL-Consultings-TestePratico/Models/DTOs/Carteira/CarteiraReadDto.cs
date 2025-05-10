using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Models.Enums;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;
using System.Text.Json.Serialization;

namespace WL_Consultings_TestePratico.Models.DTOs.Carteira
{
    public class CarteiraReadDto
    {
        public Guid Id { get; set; }

        public UsuarioReadDto Usuario { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Moeda Moeda { get; set; }
        public decimal Saldo { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusCarteira Status { get; set; }

    }
}
