using WL_Consultings_TestePratico.Models.DTOs.Transacao;

namespace WL_Consultings_TestePratico.Services.Interfaces
{
    public interface ITransacaoService
    {
        Task<TransacaoReadDto> DepositarAsync(DepositoDto request);
        Task<TransacaoReadDto> SacarAsync(SaqueDto request);
        Task<TransacaoReadDto> TransferirAsync(TransferenciaDto request);
    }
}
