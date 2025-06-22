using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class DeficienteMap : IEntityTypeConfiguration<Deficiente>
    {
        public void Configure(EntityTypeBuilder<Deficiente> builder)
        {
            builder.HasKey(e => e.IdUsuario);

            builder.ToTable("deficiente");

            builder.Property(e => e.TipoDeficiencia)
                .HasColumnName("tipoDeficiencia");

            builder.HasIndex(e => e.IdUsuario, "IX_deficiente_idUsuario");

            builder.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Deficientes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_deficiente_idUsuario_usuario_idUsuario");

        }
    }
}
