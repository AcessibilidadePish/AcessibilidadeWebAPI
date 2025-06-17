using AcessibilidadeWebAPI.BancoDados.Mapeamento;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AcessibilidadeWebAPI.BancoDados
{
    public class AcessibilidadeDbContext : DbContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
