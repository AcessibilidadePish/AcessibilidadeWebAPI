using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class VoluntarioMap : IEntityTypeConfiguration<Voluntario>
    {
        public void Configure(EntityTypeBuilder<Voluntario> builder)
        {
            builder.HasKey(e => e.IdUsuario);

            builder.ToTable("voluntario");

            builder.Property(e => e.Disponivel)
                .HasColumnName("disponivel");

            builder.Property(e => e.Avaliacao)
                .HasColumnName("avaliacao");

            builder.HasIndex(e => e.IdUsuario, "IX_voluntario_idUsuario");

            builder.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Voluntarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_voluntario_idUsuario_usuario_idUsuario");

        }
    }
}
