
using WL_Consultings_TestePratico.Models.DTOs.Autenticacao;

namespace WL_Consultings_TestePratico.Services.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<string> LoginAsync(LoginRequest request);
    }
}
