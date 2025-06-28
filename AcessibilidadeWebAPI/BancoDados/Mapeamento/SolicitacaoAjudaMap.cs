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

            builder.Property(e => e.DeficienteUsuarioId).HasColumnName("idUsuario");

            builder.Property(e => e.Descricao).HasColumnName("descricao");

            builder.Property(e => e.Status).HasColumnName("status");

            builder.Property(e => e.DataSolicitacao).HasColumnName("dataSolicitacao");

            builder.Property(e => e.DataResposta).HasColumnName("dataResposta");

            builder.Property(e => e.Latitude).HasColumnName("latitude");

            builder.Property(e => e.Longitude).HasColumnName("longitude");

            builder.Property(e => e.EnderecoReferencia).HasColumnName("enderecoReferencia");

            builder.HasOne(d => d.DeficienteUsuarioNavigation)
                .WithMany(p => p.SolicitacaoAjudas)
                .HasForeignKey(d => d.DeficienteUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_solicitacaoAjudae_idUsuario_deficiente_idUsuario");

        }
    }
}
