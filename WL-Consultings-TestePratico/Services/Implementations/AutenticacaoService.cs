using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WL_Consultings_TestePratico.Models.DTOs.Autenticacao;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Models.Exceptions;
using WL_Consultings_TestePratico.Repositories.Interfaces;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Services.Implementations
{
    public class AutenticacaoService : IAutenticacaoService
    {

        private readonly IUnityOfWork _unity;
        private readonly IMapper _mapper;
        private readonly ISenhaService _senhaService;
        private const int MAX_TENTATIVAS_LOGIN = 5;

        public AutenticacaoService(IUnityOfWork unity, IMapper mapper, ISenhaService senhaService)
        {
            _unity = unity;
            _mapper = mapper;
            _senhaService = senhaService;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var usuario = await BuscarUsuario(request.Email);
            await ValidarLoginUsuario(usuario, request.Senha);

            return GenerateAccessToken(usuario);
        }

        private async Task<Usuario> BuscarUsuario(string email)
        {
            return await _unity.UsuarioRepository.GetAsync(
                x => x.Email.Equals(email)
            ) ?? throw new NotFoundException("Usuário não encontrado.");
        }

        private async Task ValidarLoginUsuario(Usuario usuario, string senha)
        {
            if (usuario.Bloqueado)
                throw new UnauthorizedAccessException("Conta bloqueada devido a múltiplas tentativas.");

            if (!_senhaService.VerificarSenha(senha, usuario.Senha))
            {
                await AtualizarTentativasLogin(usuario);
                throw new UnauthorizedAccessException("Usuário e/ou senha errado(s)");
            }

            await ResetarTentativasLogin(usuario);
        }

        private async Task AtualizarTentativasLogin(Usuario usuario)
        {
            if (usuario.TentativasLogin < MAX_TENTATIVAS_LOGIN)
            {
                usuario.TentativasLogin++;
            }
            else
            {
                usuario.Bloqueado = true;
                usuario.TentativasLogin = 0;
            }

            await AtualizarUsuario(usuario);
        }

        private async Task ResetarTentativasLogin(Usuario usuario)
        {
            if (usuario.TentativasLogin > 0)
            {
                usuario.TentativasLogin = 0;
                await AtualizarUsuario(usuario);
            }
        }

        private async Task AtualizarUsuario(Usuario usuario)
        {
            _unity.UsuarioRepository.Update(usuario);
            await _unity.CommitAsync();
        }

        private string GenerateAccessToken(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentException("Usuário não pode ser vazio");

          
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome)
            };

            var tokenValidtyInDays = 1;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(tokenValidtyInDays),
                SigningCredentials = credentials,
                NotBefore = DateTime.UtcNow
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            token.Header["kid"] = Guid.NewGuid().ToString();

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
