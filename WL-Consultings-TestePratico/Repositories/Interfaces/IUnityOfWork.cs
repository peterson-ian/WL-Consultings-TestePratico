using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata;
using WL_Consultings_TestePratico.Models.Entities;

namespace WL_Consultings_TestePratico.Repositories.Interfaces
{
    public interface IUnityOfWork
    {
        IRepository<Usuario> UsuarioRepository { get; }
        IRepository<Carteira> CarteiraRepository { get; }
        IRepository<Transacao> TransacaoRepository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitAsync();

        void Dispose();

    }
}
