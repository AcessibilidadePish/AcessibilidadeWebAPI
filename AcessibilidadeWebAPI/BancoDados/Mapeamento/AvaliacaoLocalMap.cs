using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class AvaliacaoLocalMap : IEntityTypeConfiguration<AvaliacaoLocal>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoLocal> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("avaliacaoLocal");

            builder.Property(e => e.Id).HasColumnName("idAvaliacaoLocal");

            builder.Property(e => e.Acessivel)
                .HasColumnName("acessivel");

            builder.Property(e => e.Observacoes).HasColumnName("observacao");

            builder.Property(e => e.Timestamp).HasColumnName("timestamp");

            builder.Property(e => e.LocalId).HasColumnName("idLocal");

            builder.Property(e => e.DispositivoId).HasColumnName("idDispositivo");

            builder.HasOne(d => d.LocalNavigation)
                .WithMany(p => p.AvaliacaoLocals)
                .HasForeignKey(d => d.LocalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_avaliacaoLocal_idLocal_local_idLocal");

            builder.HasOne(d => d.DispositivoNavigation)
                .WithMany(p => p.AvaliacaoLocals)
                .HasForeignKey(d => d.DispositivoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_avaliacaoLocal_idDispositivo_dispositivo_id");
        }
    }
}
