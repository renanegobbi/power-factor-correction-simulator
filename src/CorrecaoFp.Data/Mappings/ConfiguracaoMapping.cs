using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorrecaoFp.Data.Mappings
{
    public class ConfiguracaoMapping : IEntityTypeConfiguration<Configuracao>
    {
        public void Configure(EntityTypeBuilder<Configuracao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Configuracao_ID");
            builder.Property(c => c.Nome).HasColumnName("Configuracao_Nome");
            builder.Property(c => c.Descricao).HasColumnName("Configuracao_Descricao");
            builder.Property(c => c.Valor).HasColumnName("Configuracao_Valor");
            builder.ToTable("Configuracao");
        }
    }
}