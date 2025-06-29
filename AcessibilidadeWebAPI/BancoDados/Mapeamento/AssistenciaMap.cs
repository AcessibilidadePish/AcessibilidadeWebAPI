using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class AssistenciaMap : IEntityTypeConfiguration<Assistencia>
    {
        public void Configure(EntityTypeBuilder<Assistencia> builder)
        {
            builder.HasKey(e => e.IdAssistencia);

            builder.ToTable("assistencia");

            builder.Property(e => e.IdAssistencia).HasColumnName("idAssistencia");

            builder.Property(e => e.SolicitacaoAjudaId).HasColumnName("idSolicitacaoAjuda");

            builder.Property(e => e.VoluntarioUsuarioId).HasColumnName("idUsuario");

            builder.Property(e => e.DataConclusao).HasColumnName("dataConclusao");

            builder.Property(e => e.DataAceite).HasColumnName("dataAceite");

            builder.Property(e => e.DeficienteIdUsuario).HasColumnName("deficienteIdUsuario");

            builder.HasOne(d => d.DeficienteUsuarioNavigation)
               .WithMany(p => p.Assistencias)
               .HasForeignKey(d => d.DeficienteIdUsuario)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_assistencia_deficiente_idUsuario_deficiente_idUsuario");

            builder.HasOne(d => d.VoluntarioUsuarioNavigation)
               .WithMany(p => p.Assistencias)
               .HasForeignKey(d => d.VoluntarioUsuarioId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_assistencia_idUsuario_voluntario_idUsuario");

            builder.HasOne(d => d.SolicitacaoAjudaNavigation)
               .WithMany(p => p.Assistencias)
               .HasForeignKey(d => d.SolicitacaoAjudaId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_assistencia_idSolicitacaoAjuda_solicitacaoAjuda_idSolicitacaoAjuda");
        }
    }
}
