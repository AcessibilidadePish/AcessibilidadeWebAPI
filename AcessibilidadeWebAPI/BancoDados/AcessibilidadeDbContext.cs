using AcessibilidadeWebAPI.BancoDados.Mapeamento;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AcessibilidadeWebAPI.BancoDados
{
    public class AcessibilidadeDbContext : DbContext
    {
        public AcessibilidadeDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        public virtual DbSet<Voluntario> Voluntarios { get; set; } = null!;

        public virtual DbSet<Deficiente> Deficientes { get; set; } = null!;

        public virtual DbSet<Local> Locals { get; set; } = null!;

        public virtual DbSet<AvaliacaoLocal> AvaliacaoLocals { get; set; } = null!;

        public virtual DbSet<SolicitacaoAjuda> SolicitacaoAjudas { get; set; } = null!;

        public virtual DbSet<Assistencia> Assistencias { get; set; } = null!;

        public virtual DbSet<Dispositivo> Dispositivos { get; set; } = null!;

        public virtual DbSet<HistoricoStatusSolicitacao> HistoricoStatusSolicitacao { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new VoluntarioMap());
            modelBuilder.ApplyConfiguration(new DeficienteMap());
            modelBuilder.ApplyConfiguration(new LocalMap());
            modelBuilder.ApplyConfiguration(new AvaliacaoLocalMap());
            modelBuilder.ApplyConfiguration(new SolicitacaoAjudaMap());
            modelBuilder.ApplyConfiguration(new AssistenciaMap());
            modelBuilder.ApplyConfiguration(new DispositivoMap());
            modelBuilder.ApplyConfiguration(new HistoricoStatusSolicitacaoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
