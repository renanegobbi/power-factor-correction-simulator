using CorrecaoFp.Business.Models;
using CorrecaoFp.Data.Extensions;
using CorrecaoFp.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CorrecaoFp.Data.Context
{
    public class ControladorFpContext : DbContext
    {
        public DbSet<Medicao> Medicoes { get; set; }
        public DbSet<Estagio> Estagios { get; set; }
        public DbSet<Capacitor> Capacitores { get; set; }
        public DbSet<ModoCompensacao> ModosCompensacao { get; set; }
        public DbSet<TipoPotencia> TiposPotencias { get; set; }
        public DbSet<Configuracao> DadosConfiguracao { get; set; }

        public ControladorFpContext(DbContextOptions<ControladorFpContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderExtension.AdicionaTiposPotencias(modelBuilder);
            ModelBuilderExtension.AdicionaCapacitores(modelBuilder);
            ModelBuilderExtension.AdicionaEstagios(modelBuilder);
            ModelBuilderExtension.AdicionaModoCompensacao(modelBuilder);
            ModelBuilderExtension.AdicionaMedicoes(modelBuilder);
            ModelBuilderExtension.AdicionaDadosConfiguracao(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<TipoPotencia>(new TipoPotenciaMapping());
            modelBuilder.ApplyConfiguration<Medicao>(new MedicaoMapping());
            modelBuilder.ApplyConfiguration<Capacitor>(new CapacitorMapping());
            modelBuilder.ApplyConfiguration<Estagio>(new EstagioMapping());
            modelBuilder.ApplyConfiguration<ModoCompensacao>(new ModoCompensacaoMapping());
            modelBuilder.ApplyConfiguration<Configuracao>(new ConfiguracaoMapping());
        }
    }
}
