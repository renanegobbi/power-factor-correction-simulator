using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorrecaoFp.Data.Mappings
{
    public class MedicaoMapping : IEntityTypeConfiguration<Medicao>
    {
        public void Configure(EntityTypeBuilder<Medicao> builder)
        {
            builder.HasKey(m => new { m.Id, m.DataInicio });
            builder.Property(m => m.Id).HasColumnName("Medicao_ID");
            builder.Property(m => m.DataInicio).HasColumnName("Medicao_DataInicio");
            builder.Property(m => m.DataFim).HasColumnName("Medicao_DataFim");
            builder.Property(m => m.PotenciaAtiva).HasColumnName("Medicao_PotenciaAtiva");
            builder.Property(m => m.PotenciaReativa).HasColumnName("Medicao_PotenciaReativa");
            builder.Property(m => m.PotenciaAparente).HasColumnName("Medicao_PotenciaAparente");
            builder.Property(m => m.FatorPotencia).HasColumnName("Medicao_FatorPotencia");
            builder.Property(m => m.TipoFatorPotencia).HasColumnName("Medicao_TipoFatorPotencia");
            builder.ToTable("Medicao");
        }
    }
}