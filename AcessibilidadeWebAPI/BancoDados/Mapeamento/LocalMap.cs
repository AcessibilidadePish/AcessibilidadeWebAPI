using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcessibilidadeWebAPI.BancoDados.Mapeamento
{
    public class LocalMap : IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> builder)
        {
            builder.HasKey(e => e.IdLocal);

            builder.ToTable("local");

            builder.Property(e => e.IdLocal).HasColumnName("idLocal");

            builder.Property(e => e.Latitude).HasColumnName("latitude");

            builder.Property(e => e.Longitude).HasColumnName("longitude");

            builder.Property(e => e.Descricao).HasColumnName("descricao");
            
            builder.Property(e => e.AvaliacaoAcessibilidade).HasColumnName("avaliacaoAcessibilidade");

        }
    }
}
