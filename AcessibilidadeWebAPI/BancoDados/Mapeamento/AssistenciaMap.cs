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

            builder.Property(e => e.IdSolicitacaoAjuda).HasColumnName("idSolicitacaoAjuda");

            builder.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            builder.Property(e => e.DataConclusao).HasColumnName("dataConclusao");

            builder.Property(e => e.DataAceite).HasColumnName("dataAceite");

            builder.HasOne(d => d.IdUsuarioNavigation)
               .WithMany(p => p.Assistencias)
               .HasForeignKey(d => d.IdUsuario)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_assistencia_idUsuario_deficiente_idUsuario");

            builder.HasOne(d => d.IdSolicitacaoAjudaNavigation)
               .WithMany(p => p.Assistencias)
               .HasForeignKey(d => d.IdSolicitacaoAjuda)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_assistencia_idSolicitacaoAjuda_solicitacaoAjuda_idSolicitacaoAjuda");
        }
    }
}
