using WL_Consultings_TestePratico.Models.DTOs.Paginacao;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;

namespace WL_Consultings_TestePratico.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<PaginacaoResponse<UsuarioReadDto>> GetAll(PaginacaoParametros paginacao);
        Task<UsuarioReadDto> Get(Guid id);
        Task<UsuarioReadDto> Create(UsuarioCreateDTO usuarioRequest);
        Task<UsuarioReadDto> Update(UsuarioUpdateDto usuarioRequest);
        Task Delete(Guid id);
        Task<bool> UsuarioExistsAsync(Guid id);
    }
}
