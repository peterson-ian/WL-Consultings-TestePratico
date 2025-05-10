using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WL_Consultings_TestePratico.Models.DTOs.Carteira;
using WL_Consultings_TestePratico.Models.Enums;
using System.Text.Json.Serialization;

namespace WL_Consultings_TestePratico.Models.DTOs.Transacao
{
    public class TransacaoReadDto
    {
        public string CodigoTransacao { get; set; }
        public CarteiraReadDto? CarteiraOrigem { get; set; }

        [Required]
        public CarteiraReadDto CarteiraDestino { get; set; }

        public decimal Valor { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoTransacao Tipo { get; set; }

        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataProcessamento { get; set; }

        public DateTime? DataConclusao { get; set; }

        public string Descricao { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusTransacao Status { get; set; }

        public string MotivoFalha { get; set; }
  
        public string HashVerificacao { get; set; }
    }
}
