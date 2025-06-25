using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class AvaliacaoLocalMap : IEntityTypeConfiguration<AvaliacaoLocal>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoLocal> builder)
        {
            builder.HasKey(e => e.IdAvaliacaoLocal);

            builder.ToTable("avaliacaoLocal");

            builder.Property(e => e.Acessivel)
                .HasColumnName("acessivel");

            builder.Property(e => e.Observacao).HasColumnName("observacao");

            builder.Property(e => e.Timestamp).HasColumnName("timestamp");

            builder.Property(e => e.IdLocal).HasColumnName("idLocal");


            builder.HasOne(d => d.IdLocalNavigation)
                .WithMany(p => p.AvaliacaoLocals)
                .HasForeignKey(d => d.IdLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_avaliacaoLocal_idLocal_local_idLocal");

        }
    }
}
