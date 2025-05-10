using WL_Consultings_TestePratico.Models.DTOs.Carteira;
using WL_Consultings_TestePratico.Models.Entities;

namespace WL_Consultings_TestePratico.Services.Interfaces
{
    public interface ICarteiraService
    {
        Task<CarteiraReadDto> CriarCarteiraAsync(Guid usuarioId);
        Task<CarteiraReadDto> Get(Guid id);
    }
}
