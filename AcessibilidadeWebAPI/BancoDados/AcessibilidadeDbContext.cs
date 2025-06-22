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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new VoluntarioMap());
            modelBuilder.ApplyConfiguration(new DeficienteMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
