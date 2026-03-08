using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorrecaoFp.Data.Mappings
{
    public class TipoPotenciaMapping : IEntityTypeConfiguration<TipoPotencia>
    {
        public void Configure(EntityTypeBuilder<TipoPotencia> builder)
        {
            builder.HasKey(tp => tp.Id);
            builder.Property(tp => tp.Id).HasColumnName("Tipo_Potencia_ID");
            builder.Property(tp => tp.Sigla).HasColumnName("Tipo_Potencia_Sigla");
            builder.Property(tp => tp.Descricao).HasColumnName("Tipo_Potencia_Descricao");
            builder.ToTable("Tipo_Potencia");
        }
    }
}
