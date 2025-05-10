using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WL_Consultings_TestePratico.Models.DTOs.Autenticacao;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest loginRequest)
        {
            var tokenAcesso = await _autenticacaoService.LoginAsync(loginRequest);

            return Ok(tokenAcesso);
        }

    }
}
