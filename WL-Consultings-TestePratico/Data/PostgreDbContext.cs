using Microsoft.EntityFrameworkCore;
using WL_Consultings_TestePratico.Models.Entities;

namespace WL_Consultings_TestePratico.Data
{
    public class PostgreDbContext : DbContext
    {
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carteira> Carteiras { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.CarteiraOrigem)
                .WithMany(c => c.TransferenciasOrigem)
                .HasForeignKey(t => t.CarteiraOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.CarteiraDestino)
                .WithMany(c => c.TransferenciasDestino)
                .HasForeignKey(t => t.CarteiraDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
