using CorrecaoFp.Business.Comandos.Entrada;
using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Interfaces;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Utils;
using CorrecaoFp.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrecaoFp.Data.Repository
{
    public class MedicaoRepository : Repository<Medicao>, IMedicaoRepository
    {
        public MedicaoRepository(ControladorFpContext context) : base(context) { }

        public async Task Adicionar(List<Medicao> entity)
        {
            Db.Database.ExecuteSqlRaw(Const.DELETE_FROM_Medicao);
            DbSet.AddRange(entity);
            await SaveChanges();
        }

        public async Task<Tuple<Medicao[], double>> ProcurarMedicaoReativoExcedente(ProcurarMedicaoEntrada entrada)
        {
            var totalRegistros = Convert.ToDouble(Db.Medicoes.AsNoTracking().Count());
            var paginaTamanho = ((int)entrada.PaginaTamanho == -1) ? totalRegistros : (int)entrada.PaginaTamanho;
            var somaReativosBancoCapacitores = await ObterSomaBancoCapacitor();

            var registros = entrada.Medicoes.Where(m => m.QcNecessario != 0).ToList();

            var consulta = registros
                .OrderBy(entrada)
                .Where(entrada, somaReativosBancoCapacitores);

            var numeroRegistros = consulta.Count();

            consulta = consulta
                .Skip((int)paginaTamanho * ((int)entrada.PaginaIndex - 1))
                .Take((int)paginaTamanho)
                .ToList();

            return new Tuple<Medicao[], double>(consulta.ToArray(), numeroRegistros);
        }

        public async Task<Tuple<Medicao[], double>> ProcurarMedicao(ProcurarMedicaoEntrada entrada)
        {
            var totalRegistros = Convert.ToDouble(Db.Medicoes.AsNoTracking().Count());
            var paginaTamanho = ((int)entrada.PaginaTamanho == -1) ? totalRegistros : (int)entrada.PaginaTamanho;
            var somaReativosBancoCapacitores = await ObterSomaBancoCapacitor();

            var registros = Db.Medicoes
                .AsNoTracking()
                .OrderBy(entrada)
                .Skip((int)paginaTamanho * ((int)entrada.PaginaIndex - 1))
                .Take((int)paginaTamanho)
                .Where(entrada, somaReativosBancoCapacitores).ToList();

            return new Tuple<Medicao[], double>(registros.ToArray(), totalRegistros);
        }

        public async Task<double> ObterSomaBancoCapacitor()
        {
            var reativos = await Db.Capacitores.ToListAsync();
            var somaReativos = reativos.Select(e => e.Potencia).Sum();
            return somaReativos;
        }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<Medicao> OrderBy(this IEnumerable<Medicao> registros, ProcurarMedicaoEntrada entrada)
        {
            switch (entrada.OrdenarPor)
            {
                case MedicaoOrdenarPor.DataFim:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.DataFim)
                        : registros.OrderBy(e => e.DataFim);
                    break;
                case MedicaoOrdenarPor.PotenciaAtivaTotal:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.PotenciaAtiva)
                        : registros.OrderBy(e => e.PotenciaAtiva);
                    break;
                case MedicaoOrdenarPor.PotenciaReativaTotal:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.PotenciaReativa)
                        : registros.OrderBy(e => e.PotenciaReativa);
                    break;
                case MedicaoOrdenarPor.PotenciaAparenteSomaVetorial:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.PotenciaAparente)
                        : registros.OrderBy(e => e.PotenciaAparente);
                    break;
                case MedicaoOrdenarPor.PotenciaAparenteAritmetica:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.PotenciaAparente)
                        : registros.OrderBy(e => e.PotenciaAparente);
                    break;
                case MedicaoOrdenarPor.FatorPotencia:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.FatorPotencia)
                        : registros.OrderBy(e => e.FatorPotencia);
                    break;
                case MedicaoOrdenarPor.FpRealMedia:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.FatorPotencia)
                        : registros.OrderBy(e => e.FatorPotencia);
                    break;
                case MedicaoOrdenarPor.IndMedia:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.TipoFatorPotencia)
                        : registros.OrderBy(e => e.TipoFatorPotencia);
                    break;
                case MedicaoOrdenarPor.QcNecessario:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.QcNecessario)
                        : registros.OrderBy(e => e.QcNecessario);
                    break;
                case MedicaoOrdenarPor.FatorPotenciaCorrigido:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.FpFinal)
                        : registros.OrderBy(e => e.FpFinal);
                    break;
                case MedicaoOrdenarPor.Acao:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.TipoFatorPotencia)
                        : registros.OrderBy(e => e.TipoFatorPotencia);
                    break;
                case MedicaoOrdenarPor.IndFpCorrigido:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.TipoFatorPotenciaCorrigido)
                        : registros.OrderBy(e => e.TipoFatorPotenciaCorrigido);
                    break;
                default:
                    registros = entrada.OrdenarSentido.ToUpper() == Const.ORDEM_DESCENDENTE
                        ? registros.OrderByDescending(e => e.DataInicio)
                        : registros.OrderBy(e => e.DataInicio);
                    break;
            }

            return registros;
        }

        public static IEnumerable<Medicao> Where(this IEnumerable<Medicao> registros, ProcurarMedicaoEntrada entrada, double somaReativosBancoCapacitores)
        {
            if (entrada.DataInicio.HasValue)
            {
                registros = registros.Where(e => e.DataInicio.Date == entrada.DataInicio?.Date);
            }

            if (entrada.DataFim.HasValue)
            {
                registros = registros.Where(e => e.DataFim.Date == entrada.DataFim?.Date);
            }

            if (entrada.FpRealMedia.HasValue)
            {
                registros = registros.Where(e => e.FatorPotencia == entrada.FpRealMedia);
            }

            if (entrada.IndMedia == IndMedia.Capacitivo || entrada.IndMedia == IndMedia.Indutivo)
            {
                var indMedia = (entrada.IndMedia == IndMedia.Capacitivo) ? Const.Cap : Const.Ind;
                registros = registros.Where(e => e.TipoFatorPotencia == indMedia);
            }

            if (entrada.Acao == OperacaoEstagio.Ativar || entrada.Acao == OperacaoEstagio.Desativar)
            {
                var indMedia = (entrada.Acao == OperacaoEstagio.Desativar) ? Const.Cap : Const.Ind;
                registros = registros.Where(e => e.TipoFatorPotencia == indMedia);
            }

            return registros;
        }
    }
}
