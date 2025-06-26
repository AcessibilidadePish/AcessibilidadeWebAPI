using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class SolicitacaoAjudaMap : IEntityTypeConfiguration<SolicitacaoAjuda>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAjuda> builder)
        {
            builder.HasKey(e => e.IdSolicitacaoAjuda);

            builder.ToTable("solicitacaoAjuda");

            builder.Property(e => e.IdSolicitacaoAjuda)
                .HasColumnName("idSolicitacaoAjuda");

            builder.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            builder.Property(e => e.Descricao).HasColumnName("descricao");

            builder.Property(e => e.Status).HasColumnName("status");

            builder.Property(e => e.DataSolicitacao).HasColumnName("dataSolicitacao");

            builder.Property(e => e.DataResposta).HasColumnName("dataResposta");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.SolicitacaoAjudas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_solicitacaoAjudae_idUsuario_deficiente_idUsuario");

        }
    }
}
