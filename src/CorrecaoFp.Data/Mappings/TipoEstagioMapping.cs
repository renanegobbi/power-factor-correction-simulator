using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorrecaoFp.Data.Mappings
{
    public class TipoEstagioMapping : IEntityTypeConfiguration<TipoEstagio>
    {
        public void Configure(EntityTypeBuilder<TipoEstagio> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Tipo_Estagio_ID");
            builder.Property(e => e.Nome).HasColumnName("Tipo_Estagio_Nome");
            builder.Property(e => e.Descricao).HasColumnName("Tipo_Estagio_Descricao");
            builder.ToTable("Tipo_Estagio");
        }
    }
}