using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WL_Consultings_TestePratico.Models.DTOs.Carteira;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Models.Enums;
using WL_Consultings_TestePratico.Models.Exceptions;
using WL_Consultings_TestePratico.Repositories.Interfaces;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Services.Implementations
{
    public class CarteiraService : ICarteiraService
    {
        private readonly IUnityOfWork _unity;
        private readonly IMapper _mapper;

        public CarteiraService(IUnityOfWork unity, IMapper mapper)
        {
            _unity = unity;
            _mapper = mapper;
        }


        public async Task<CarteiraReadDto> CriarCarteiraAsync(Guid usuarioId)
        {
            var carteira = new Carteira
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuarioId,
                Moeda = "BRL",
                Saldo = 0,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow,
                Status = StatusCarteira.ATIVA.ToString()
            };

            _unity.CarteiraRepository.Create(carteira);
            await _unity.CommitAsync();

            CarteiraReadDto carteiraResponse = _mapper.Map<CarteiraReadDto>(carteira);
            return carteiraResponse;
        }

        public async Task<CarteiraReadDto> Get(Guid id)
        {
            Carteira carteira = await _unity.CarteiraRepository.GetAsync(x => x.Id.Equals(id), includes: "Usuario")
                ?? throw new NotFoundException("Carteira não encontrada.");

            CarteiraReadDto carteiraResponse = _mapper.Map<CarteiraReadDto>(carteira);
            return carteiraResponse;
        }
    }
}
