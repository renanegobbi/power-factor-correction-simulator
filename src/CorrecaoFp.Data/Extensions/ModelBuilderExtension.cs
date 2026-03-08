using CorrecaoFp.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CorrecaoFp.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder AdicionaTiposPotencias(this ModelBuilder builder)
        {
            builder.Entity<TipoPotencia>().HasData(

               new TipoPotencia { Id = 1, Sigla = "C", Descricao = "Capacitor Trifásico" },
               new TipoPotencia { Id = 2, Sigla = "L", Descricao = "Reator de Derivação Trifásico" }
             );

            return builder;
        }

        public static ModelBuilder AdicionaCapacitores(this ModelBuilder builder)
        {
            Capacitor cap = new Capacitor { Id = 1, Fabricante = "WEG", Potencia = 0.5, Tensao = 220, Unidade = "kVAr" };

            builder.Entity<Capacitor>().HasData(

               new Capacitor { Id = 1, Fabricante = "WEG", Potencia = 0, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 2, Fabricante = "WEG", Potencia = 0.5, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 3, Fabricante = "WEG", Potencia = 0.75, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 4, Fabricante = "WEG", Potencia = 1, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 5, Fabricante = "WEG", Potencia = 1.5, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 6, Fabricante = "WEG", Potencia = 2, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 7, Fabricante = "WEG", Potencia = 2.5, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 8, Fabricante = "WEG", Potencia = 3, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 9, Fabricante = "WEG", Potencia = 5, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 10, Fabricante = "WEG", Potencia = 7.5, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 11, Fabricante = "WEG", Potencia = 10, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 12, Fabricante = "WEG", Potencia = 12, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 13, Fabricante = "WEG", Potencia = 15, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 14, Fabricante = "WEG", Potencia = 17, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 15, Fabricante = "WEG", Potencia = 20, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 16, Fabricante = "WEG", Potencia = 25, Tensao = 220, Unidade = "kVAr" },
               new Capacitor { Id = 17, Fabricante = "WEG", Potencia = 30, Tensao = 220, Unidade = "kVAr" }
             );

            return builder;
        }

        public static ModelBuilder AdicionaEstagios(this ModelBuilder builder)
        {
            Capacitor cap = new Capacitor { Id = 1, Fabricante = "WEG", Potencia = 0.5, Tensao = 220, Unidade = "kVAr" };

            builder.Entity<Estagio>().HasData(

               new Estagio { Id = 1, TipoPotenciaId = 1, CapacitorId = cap.Id, Descricao = "Potência do estárgio 1" },
               new Estagio { Id = 2, TipoPotenciaId = 1, CapacitorId = 2, Descricao = "Potência do estárgio 2" },
               new Estagio { Id = 3, TipoPotenciaId = 1, CapacitorId = 3, Descricao = "Potência do estárgio 3" },
               new Estagio { Id = 4, TipoPotenciaId = 1, CapacitorId = 4, Descricao = "Potência do estárgio 4" },
               new Estagio { Id = 5, TipoPotenciaId = 1, CapacitorId = 5, Descricao = "Potência do estárgio 5" },
               new Estagio { Id = 6, TipoPotenciaId = 1, CapacitorId = 6, Descricao = "Potência do estárgio 6" },
               new Estagio { Id = 7, TipoPotenciaId = 1, CapacitorId = 7, Descricao = "Potência do estárgio 7" },
               new Estagio { Id = 8, TipoPotenciaId = 1, CapacitorId = 8, Descricao = "Potência do estárgio 8" },
               new Estagio { Id = 9, TipoPotenciaId = 1, CapacitorId = 9, Descricao = "Potência do estárgio 9" },
               new Estagio { Id = 10, TipoPotenciaId = 1, CapacitorId = 10, Descricao = "Potência do estárgio 10" },
               new Estagio { Id = 11, TipoPotenciaId = 1, CapacitorId = 11, Descricao = "Potência do estárgio 11" },
               new Estagio { Id = 12, TipoPotenciaId = 1, CapacitorId = 12, Descricao = "Potência do estárgio 12" },
               new Estagio { Id = 13, TipoPotenciaId = 1, CapacitorId = 13, Descricao = "Potência do estárgio 13" },
               new Estagio { Id = 14, TipoPotenciaId = 1, CapacitorId = 14, Descricao = "Potência do estárgio 14" },
               new Estagio { Id = 15, TipoPotenciaId = 1, CapacitorId = 15, Descricao = "Potência do estárgio 15" },
               new Estagio { Id = 16, TipoPotenciaId = 1, CapacitorId = 16, Descricao = "Potência do estárgio 16" },
               new Estagio { Id = 17, TipoPotenciaId = 1, CapacitorId = 17, Descricao = "Potência do estárgio 17" },
               new Estagio { Id = 18, TipoPotenciaId = 1, CapacitorId = 1, Descricao = "Potência do estárgio 18" },
               new Estagio { Id = 19, TipoPotenciaId = 1, CapacitorId = 1, Descricao = "Potência do estárgio 19" },
               new Estagio { Id = 20, TipoPotenciaId = 1, CapacitorId = 1, Descricao = "Potência do estárgio 20" },
               new Estagio { Id = 21, TipoPotenciaId = 1, CapacitorId = 1, Descricao = "Potência do estárgio 21" },
               new Estagio { Id = 22, TipoPotenciaId = 1, CapacitorId = 1, Descricao = "Potência do estárgio 22" },
               new Estagio { Id = 23, TipoPotenciaId = 1, CapacitorId = 1, Descricao = "Potência do estárgio 23" },
               new Estagio { Id = 24, TipoPotenciaId = 1, CapacitorId = 1, Descricao = "Potência do estárgio 24" },
               new Estagio { Id = 25, TipoPotenciaId = 1, CapacitorId = 12, Descricao = "Potência do estágio fixo 1" },
               new Estagio { Id = 26, TipoPotenciaId = 1, CapacitorId = 13, Descricao = "Potência do estágio fixo 2" },
               new Estagio { Id = 27, TipoPotenciaId = 1, CapacitorId = 13, Descricao = "Potência do estágio fixo 3" },
               new Estagio { Id = 28, TipoPotenciaId = 1, CapacitorId = 13, Descricao = "Potência do estágio fixo 4" }
             );

            return builder;
        }

        public static ModelBuilder AdicionaModoCompensacao(this ModelBuilder builder)
        {
            builder.Entity<ModoCompensacao>().HasData(

               new ModoCompensacao { Id = 1, Nome = "PFW03-M12 (Modo inteligente)" },
               new ModoCompensacao { Id = 2, Nome = "Sequencial Ascendente" },
               new ModoCompensacao { Id = 3, Nome = "Sequencial Descendente" },
               new ModoCompensacao { Id = 4, Nome = "Linear" },
               new ModoCompensacao { Id = 5, Nome = "Circular" },
               new ModoCompensacao { Id = 6, Nome = "Manual" }
             );

            return builder;
        }

        public static ModelBuilder AdicionaMedicoes(this ModelBuilder builder)
        {
            builder.Entity<Medicao>().HasData(

               new Medicao
               {
                   Id = 1,
                   DataInicio = new DateTime(2023, 06, 01, 8, 15, 10),
                   DataFim = new DateTime(2023, 06, 01, 8, 30, 10),
                   PotenciaAtiva = 14326.78,
                   PotenciaReativa = -2881.58,
                   PotenciaAparente = 14769.8,
                   FatorPotencia = 0.97,
                   TipoFatorPotencia = "Cap"
               }
             );

            return builder;
        }

        public static ModelBuilder AdicionaDadosConfiguracao(this ModelBuilder builder)
        {
            builder.Entity<Configuracao>().HasData(

               new Configuracao { Id = 1, Nome = "QUANTIDADE_ESTAGIOS_FIXOS", Descricao = "Quantidade de estágios fixos", Valor = 4 },
               new Configuracao { Id = 2, Nome = "QUANTIDADE_ESTAGIOS_AUTOMATICOS", Descricao = "Quantidade de estágios automáticos", Valor = 12 },
               new Configuracao { Id = 3, Nome = "TENSAO_LINHA", Descricao = "Tensão de linha (V)", Valor = 127 },
               new Configuracao { Id = 4, Nome = "RELACAO_TC", Descricao = "Relação de transformação do transformador de corrente (TC)", Valor = 800 }
             );

            return builder;
        }

    }
}