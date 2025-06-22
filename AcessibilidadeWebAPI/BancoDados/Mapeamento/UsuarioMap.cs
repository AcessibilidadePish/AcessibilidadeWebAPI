using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.IdUsuario);

            builder.ToTable("usuario");

            builder.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            builder.Property(e => e.Email)
                .HasColumnName("email");

            builder.Property(e => e.Nome)
                .HasColumnName("nome");

            builder.Property(e => e.Telefone)
                .HasColumnName("telefone");

            builder.Property(e => e.Senha)
                .HasColumnName("senha");
        }
    }
}
