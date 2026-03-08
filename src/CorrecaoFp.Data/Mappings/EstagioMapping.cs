using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorrecaoFp.Data.Mappings
{
    public class EstagioMapping : IEntityTypeConfiguration<Estagio>
    {
        public void Configure(EntityTypeBuilder<Estagio> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Estagio_ID");
            builder.Property(e => e.TipoPotenciaId).HasColumnName("Tipo_Potencia_ID");
            builder.Property(e => e.CapacitorId).HasColumnName("Capacitor_ID");
            builder.Property(e => e.Descricao).HasColumnName("Estagio_Descricao");
            builder.ToTable("Estagio");
        }
    }
}