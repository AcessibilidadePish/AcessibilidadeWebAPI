using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class DispositivoMap : IEntityTypeConfiguration<Dispositivo>
    {
        public void Configure(EntityTypeBuilder<Dispositivo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("dispositivo");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.NumeroSerie).HasColumnName("numeroSerie");

            builder.Property(e => e.DataRegistro).HasColumnName("dataRegistro");

            builder.Property(e => e.UsuarioProprietarioId).HasColumnName("usuarioProprietarioId");

            builder.HasOne(d => d.UsuarioProprietarioNavigation)
                .WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.UsuarioProprietarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dispositivo_usuarioProprietarioId_usuario_idUsuario");
        }
    }
} 