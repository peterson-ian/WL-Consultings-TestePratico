using AutoMapper;
using WL_Consultings_TestePratico.Models.DTOs.Paginacao;
using WL_Consultings_TestePratico.Models.DTOs.Usuario;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Models.Enums;
using WL_Consultings_TestePratico.Models.Exceptions;
using WL_Consultings_TestePratico.Models.Extensions;
using WL_Consultings_TestePratico.Repositories.Implementations;
using WL_Consultings_TestePratico.Repositories.Interfaces;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnityOfWork _unity;
        private readonly ICarteiraService _carteiraService;
        private readonly ISenhaService _senhaService;
        private readonly IMapper _mapper;

        public UsuarioService(IUnityOfWork unity, ICarteiraService carteiraService, ISenhaService senhaService, IMapper mapper)
        {
            _unity = unity;
            _carteiraService = carteiraService;
            _senhaService = senhaService;
            _mapper = mapper;
        }

        public async Task<UsuarioReadDto> Create(UsuarioCreateDTO usuarioRequest)
        {
            await using var transaction = await _unity.BeginTransactionAsync();
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(usuarioRequest);
                usuario.Senha = _senhaService.GerarHashSenha(usuario.Senha);

                _unity.UsuarioRepository.Create(usuario);
                await _unity.CommitAsync();

                await _carteiraService.CriarCarteiraAsync(usuario.Id);

                await transaction.CommitAsync();

                UsuarioReadDto usuarioResponse = _mapper.Map<UsuarioReadDto>(usuario);
                return usuarioResponse;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            await using var transaction = await _unity.BeginTransactionAsync();
            try
            {
                Usuario usuario = await _unity.UsuarioRepository.GetAsync(x => x.Id.Equals(id))
                    ?? throw new NotFoundException("Usuário não encontrado.");

                usuario.Ativo = false;
                 _unity.UsuarioRepository.Update(usuario);

                Carteira carteira = await _unity.CarteiraRepository.GetAsync(x => x.UsuarioId.Equals(id))
                    ?? throw new NotFoundException("Carteira não encontrado.");

                carteira.Status = StatusCarteira.INATIVA.ToString();
                _unity.CarteiraRepository.Update(carteira);

                await _unity.CommitAsync();
                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<UsuarioReadDto> Get(Guid id)
        {
            Usuario usuario = await _unity.UsuarioRepository.GetAsync(x => x.Id.Equals(id), includes: "Carteira")
                ?? throw new NotFoundException("Usuário não encontrado.");

            UsuarioReadDto usuarioResponse = _mapper.Map<UsuarioReadDto>(usuario);
            return usuarioResponse;
        }

        public async Task<PaginacaoResponse<UsuarioReadDto>> GetAll(PaginacaoParametros paginacao)
        {
            var query = await _unity.UsuarioRepository.GetAllAsync(includes: "Carteira");

            return await query.ToPaginatedResultAsync<Usuario, UsuarioReadDto>(
                paginacao.NumeroPagina,
                paginacao.TamanhoPagina,
                _mapper.ConfigurationProvider);

        }

        public async Task<UsuarioReadDto> Update(UsuarioUpdateDto usuarioRequest)
        {
            Usuario usuario = await _unity.UsuarioRepository.GetAsync(x => x.Id.Equals(usuarioRequest.Id))
               ?? throw new NotFoundException("Usuário não encontrado.");

            _mapper.Map(usuarioRequest, usuario);

            _unity.UsuarioRepository.Update(usuario);
            await _unity.CommitAsync();

            UsuarioReadDto usuarioResponse = _mapper.Map<UsuarioReadDto>(usuario);
            return usuarioResponse;
        }

        public async Task<bool> UsuarioExistsAsync(Guid id)
        {
            return await _unity.UsuarioRepository.ExistsAsync(x => x.Id.Equals(id));
        }
    }
}
