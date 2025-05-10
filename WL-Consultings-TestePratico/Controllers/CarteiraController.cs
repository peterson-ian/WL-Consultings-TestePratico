using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WL_Consultings_TestePratico.Models.DTOs.Carteira;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;
using WL_Consultings_TestePratico.Services.Implementations;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarteiraController : Controller
    {
        private readonly ICarteiraService _carteiraService;

        public CarteiraController(ICarteiraService carteiraService)
        {
            _carteiraService = carteiraService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarteiraReadDto>> GetAsync(Guid id)
        {
            CarteiraReadDto carteira = await _carteiraService.Get(id);

            return Ok(carteira);
        }
    }
}
