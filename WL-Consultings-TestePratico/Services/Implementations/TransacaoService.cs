using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using WL_Consultings_TestePratico.Models.DTOs.Transacao;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Models.Enums;
using WL_Consultings_TestePratico.Repositories.Interfaces;
using WL_Consultings_TestePratico.Services.Interfaces;

namespace WL_Consultings_TestePratico.Services.Implementations
{
    public class TransacaoService : ITransacaoService
    {
        private readonly IUnityOfWork _unity;
        private readonly IMapper _mapper;

        public TransacaoService(IUnityOfWork unity, IMapper mapper)
        {
            _unity = unity;
            _mapper = mapper;
        }

        public async Task<TransacaoReadDto> DepositarAsync(DepositoDto request)
        {
            await using var transaction = await _unity.BeginTransactionAsync();
            try
            {
                var carteira = await _unity.CarteiraRepository.GetAsync(x=> x.Id.Equals(request.CarteiraId) && x.Status.Equals("ATIVA"), includes: "Usuario")
                    ?? throw new Exception("Carteira não encontrada");

                carteira.Saldo += request.Valor;
                carteira.DataAtualizacao = DateTime.UtcNow;

                var transacao = new Transacao
                {
                    Id = Guid.NewGuid(),
                    CodigoTransacao = Guid.NewGuid().ToString(),
                    CarteiraDestinoId = carteira.Id,
                    CarteiraDestino = carteira,
                    Tipo  = TipoTransacao.DEPOSITO.ToString(),
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    DataSolicitacao = DateTime.UtcNow,
                    Status = StatusTransacao.CONCLUIDA.ToString(),
                };

                transacao.HashVerificacao = GerarHash(carteira.Id, carteira.Id, request.Valor, transacao.Id.ToString());

                _unity.CarteiraRepository.Update(carteira);
                _unity.TransacaoRepository.Create(transacao);
                await _unity.CommitAsync();
                await transaction.CommitAsync();

                TransacaoReadDto transacaoResponse = _mapper.Map<TransacaoReadDto>(transacao);
                return transacaoResponse;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TransacaoReadDto> SacarAsync(SaqueDto request)
        {
            await using var transaction = await _unity.BeginTransactionAsync();
            try
            {
                var carteira = await _unity.CarteiraRepository.GetAsync(x => x.Id.Equals(request.CarteiraId) && x.Status.Equals("ATIVA"), includes: "Usuario")
                    ?? throw new Exception("Carteira não encontrada");

                if (carteira.Saldo < request.Valor)
                    throw new Exception("Saldo insuficiente para realizar o saque");

                carteira.Saldo -= request.Valor;
                carteira.DataAtualizacao = DateTime.UtcNow;

                var transacao = new Transacao
                {
                    Id = Guid.NewGuid(),
                    CodigoTransacao = Guid.NewGuid().ToString(),
                    CarteiraDestinoId = carteira.Id,
                    Tipo = TipoTransacao.SAQUE.ToString(),
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    DataSolicitacao = DateTime.UtcNow,
                    Status = StatusTransacao.CONCLUIDA.ToString(),
                };

                transacao.HashVerificacao = GerarHash(carteira.Id, carteira.Id, request.Valor, transacao.Id.ToString());

                _unity.CarteiraRepository.Update(carteira);
                _unity.TransacaoRepository.Create(transacao);
                await _unity.CommitAsync();
                await transaction.CommitAsync();

                TransacaoReadDto transacaoResponse = _mapper.Map<TransacaoReadDto>(transacao);
                return transacaoResponse;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TransacaoReadDto> TransferirAsync(TransferenciaDto request)
        {
            await using var transaction = await _unity.BeginTransactionAsync();
            try
            {
                var carteiraOrigem = await _unity.CarteiraRepository.GetAsync(x => x.Id.Equals(request.CarteiraIdOrigem) && x.Status.Equals("ATIVA") , includes: "Usuario")
                  ?? throw new Exception("Carteira origem não encontrada");

                if (carteiraOrigem.Saldo < request.Valor)
                    throw new Exception("Saldo insuficiente para realizar a transferência");

                carteiraOrigem.Saldo -= request.Valor;
                carteiraOrigem.DataAtualizacao = DateTime.UtcNow;

                var carteiraDestino = await _unity.CarteiraRepository.GetAsync(x => x.Id.Equals(request.CarteiraIdDestino) && x.Status.Equals("ATIVA"), includes: "Usuario")
                    ?? throw new Exception("Carteira destino não encontrada");

                carteiraDestino.Saldo += request.Valor;
                carteiraDestino.DataAtualizacao = DateTime.UtcNow;

                var transacao = new Transacao
                {
                    Id = Guid.NewGuid(),
                    CodigoTransacao = Guid.NewGuid().ToString(),
                    CarteiraOrigemId = carteiraOrigem.Id,
                    CarteiraDestinoId = carteiraDestino.Id,
                    Tipo = TipoTransacao.TRANSFERENCIA.ToString(),
                    Valor = request.Valor,
                    Descricao = request.Descricao,
                    DataSolicitacao = DateTime.UtcNow,
                    Status = StatusTransacao.CONCLUIDA.ToString(),
                };

                transacao.HashVerificacao = GerarHash(carteiraOrigem.Id, carteiraDestino.Id, request.Valor, transacao.Id.ToString());

                _unity.CarteiraRepository.Update(carteiraOrigem);
                _unity.CarteiraRepository.Update(carteiraDestino);
                _unity.TransacaoRepository.Create(transacao);
                await _unity.CommitAsync();
                await transaction.CommitAsync();

                TransacaoReadDto transacaoResponse = _mapper.Map<TransacaoReadDto>(transacao);
                return transacaoResponse;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private string GerarHash(Guid origem, Guid destino, decimal valor, string chaveSecreta)
        {
            var mensagem = $"{origem}-{destino}-{valor}-{DateTime.UtcNow.Ticks}";

            var keyBytes = Encoding.UTF8.GetBytes(chaveSecreta);
            var messageBytes = Encoding.UTF8.GetBytes(mensagem);

            using var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(messageBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
