using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorrecaoFp.Data.Mappings
{
    public class ModoCompensacaoMapping : IEntityTypeConfiguration<ModoCompensacao>
    {
        public void Configure(EntityTypeBuilder<ModoCompensacao> builder)
        {
            builder.HasKey(mc => mc.Id);
            builder.Property(mc => mc.Id).HasColumnName("Modo_Compensacao_ID");
            builder.Property(mc => mc.Nome).HasColumnName("Modo_Compensacao_Nome");
            builder.ToTable("Modo_Compensacao");
        }
    }
}