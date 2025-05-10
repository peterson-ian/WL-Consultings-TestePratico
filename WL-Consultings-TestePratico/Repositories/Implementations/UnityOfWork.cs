using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using WL_Consultings_TestePratico.Repositories.Interfaces;
using WL_Consultings_TestePratico.Models.Entities;
using WL_Consultings_TestePratico.Data;

namespace WL_Consultings_TestePratico.Repositories.Implementations
{
    public class UnityOfWork : IUnityOfWork
    {
        public readonly PostgreDbContext _context;

        public UnityOfWork(PostgreDbContext context)
        {
            _context = context;
        }

        private IRepository<Usuario> _usuarioRepository;
        public IRepository<Usuario> UsuarioRepository => _usuarioRepository ??= new Repository<Usuario>(_context);

        private IRepository<Carteira> _carteiraRepository;
        public IRepository<Carteira> CarteiraRepository => _carteiraRepository ??= new Repository<Carteira>(_context);

        private IRepository<Transacao> _transacaoRepository;
        public IRepository<Transacao> TransacaoRepository => _transacaoRepository ??= new Repository<Transacao>(_context);

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
