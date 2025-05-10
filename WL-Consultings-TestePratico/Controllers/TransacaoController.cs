using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WL_Consultings_TestePratico.Models.DTOs.Transacao;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;
using WL_Consultings_TestePratico.Services.Implementations;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost("depositar")]
        public async Task<ActionResult<TransacaoReadDto>> DepositarAsync(DepositoDto request)
        {
            var deposito = await _transacaoService.DepositarAsync(request);

            return Ok(deposito);
        }

        [HttpPost("sacar")]
        public async Task<ActionResult<TransacaoReadDto>> SacarAsync(SaqueDto request)
        {
            var saque = await _transacaoService.SacarAsync(request);

            return Ok(saque);
        }

        [HttpPost("transferir")]
        public async Task<ActionResult<TransacaoReadDto>> TransferirAsync(TransferenciaDto request)
        {
            var transferencia = await _transacaoService.TransferirAsync(request);

            return Ok(transferencia);
        }
    }
}
