using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WL_Consultings_TestePratico.Models.DTOs.Paginacao;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Models.Exceptions;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginacaoResponse<UsuarioReadDto>>> GetAsync([FromQuery] PaginacaoParametros paginacao)
        {
            PaginacaoResponse<UsuarioReadDto> usuarios = await _usuarioService.GetAll(paginacao);
            
            return Ok(usuarios);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioReadDto>> GetAsync(Guid id)
        {
            UsuarioReadDto usuario = await _usuarioService.Get(id);

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioReadDto>> PostAsync(UsuarioCreateDTO usuarioRequest)
        {
            var usuario = await _usuarioService.Create(usuarioRequest);

            return Ok(usuario);
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioReadDto>> PutAsync(UsuarioUpdateDto usuarioRequest)
        {
            var usuario = await _usuarioService.Update(usuarioRequest);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _usuarioService.Delete(id);

            return Ok();
        }

    }
}
