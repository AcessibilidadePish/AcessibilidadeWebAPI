using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class HistoricoStatusSolicitacaoMap : IEntityTypeConfiguration<HistoricoStatusSolicitacao>
    {
        public void Configure(EntityTypeBuilder<HistoricoStatusSolicitacao> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("historicoStatusSolicitacao");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.SolicitacaoAjudaId).HasColumnName("solicitacaoAjudaId");

            builder.Property(e => e.StatusAnterior).HasColumnName("statusAnterior");

            builder.Property(e => e.StatusAtual).HasColumnName("statusAtual");

            builder.Property(e => e.DataMudanca).HasColumnName("dataMudanca");

            builder.HasOne(d => d.SolicitacaoAjudaNavigation)
                .WithMany(p => p.HistoricoStatusSolicitacao)
                .HasForeignKey(d => d.SolicitacaoAjudaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_historicoStatusSolicitacao_solicitacaoAjudaId_solicitacaoAjuda_idSolicitacaoAjuda");
        }
    }
} 