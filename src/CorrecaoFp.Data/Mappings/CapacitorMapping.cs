using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorrecaoFp.Data.Mappings
{
    public class CapacitorMapping : IEntityTypeConfiguration<Capacitor>
    {
        public void Configure(EntityTypeBuilder<Capacitor> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("Capacitor_ID");
            builder.Property(c => c.Fabricante).HasColumnName("Capacitor_Fabricante");
            builder.Property(c => c.Potencia).HasColumnName("Capacitor_Potencia");
            builder.Property(c => c.Tensao).HasColumnName("Capacitor_Tensao");
            builder.Property(c => c.Unidade).HasColumnName("Capacitor_Unidade");
            builder.ToTable("Capacitor");
        }
    }
}