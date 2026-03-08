using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CorrecaoFp.Business.Utils
{
    public static class BancoCapacitorUtil
    {
        public static double CalcularPotenciaAtiva(Medicao medicao)
        {
            return Math.Sqrt(Math.Pow(medicao.PotenciaAparente, 2) - Math.Pow(medicao.PotenciaReativa, 2)).ArredondarDuasCasasDecimais();
        }

        public static double SomarCapacitores(List<double> estagiosOrderAsc)
        {
            return estagiosOrderAsc.Sum();
        }

        public static double QcNecessario(double potenciaAtiva, double fpInicial, double fpDesejado)
        {
            var qcNecessario = potenciaAtiva * (Math.Tan(Math.Acos(fpInicial)) - Math.Tan(Math.Acos(fpDesejado)));

            return Math.Round(qcNecessario, 2);
        }

        public static List<double> UtilizarBancoCapacitores(IList<double> estagios, double qcNecessario, double quantidadeEstagios, Medicao medicao, double ck)
        {
            if (SomaEstagiosMenorQueQcNecessario(estagios, qcNecessario))
            {
                medicao.TornarFpCorrigidoIgualFalso();
                medicao.AdicionarTodosEstagios(estagios);
                return medicao.EstagiosNecessarios; 
            }

            double somatorioQc = 0;

            if (CondicaoParada(qcNecessario, ck, somatorioQc)) { return medicao.EstagiosNecessarios; }

            for (int posicaoEstagios = 0; posicaoEstagios < quantidadeEstagios; posicaoEstagios++)
            {
                if (estagios[posicaoEstagios] == 0) { continue; }

                medicao.EstagiosNecessarios.Add(estagios[posicaoEstagios]);

                somatorioQc += estagios[posicaoEstagios];

                if (CondicaoParada(qcNecessario, ck, somatorioQc)) { break; }
            }

            return medicao.EstagiosNecessarios;
        }

        private static bool CondicaoParada(double qcNecessario, double ck, double somatorioQc)
        {
            //Finalizo quando atinjo o valor de reativo necessário ou quando o ck é menor que o mínimo necessário para ativar ou desativar qualquer estágio
            return somatorioQc >= qcNecessario || (somatorioQc - qcNecessario).ObterValorAbsoluto() < ck;
        }

        private static void TornarFpCorrigidoIgualFalso(this Medicao medicao)
        {
            medicao.FpCorrigido = false;
        }

        private static void AdicionarTodosEstagios(this Medicao medicao, IList<double> estagios)
        {
            medicao.EstagiosNecessarios.AddRange(estagios.Where(valorEstagio => valorEstagio != 0));
        }

        private static bool SomaEstagiosMenorQueQcNecessario(this IList<double> estagios, double qcNecessario/*, Medicao medicao*/)
        {
            if (estagios.Sum() < qcNecessario)
            {
                return true;
            }

            return false;
        }

        public static double RecalcularFp(List<double> capacitoresUtilizados, Medicao medicao)
        {
            var potenciaReativaAdicionada = capacitoresUtilizados.Sum();

            medicao.FpFinal = Math.Round(Math.Cos(
                Math.Atan((medicao.PotenciaAtiva * Math.Tan(Math.Acos(medicao.FatorPotencia)) - Math.Abs(potenciaReativaAdicionada)) / (medicao.PotenciaAtiva))), 2);

            return medicao.FpFinal;
        }

        public static List<Medicao> CompensacaoSequencialAscendente(
            List<Medicao> medicoes,
            List<EstagioResumo> estagios,
            double ck,
            double fpDesejado = Const.FP_0_92,
            double quantidadeEstagios = Const.QUANTIDADE_ESTAGIO_AUTOMATICO_DEFAULT)
        {
            var estagiosOrderAsc = BancoCapacitorUtil.OrdenarEstagiosPorPotencia(estagios, OrdenarPor.ModoAscendente);

            foreach (var medicao in medicoes)
            {
                if (medicao.FatorPotencia >= fpDesejado) { medicao.FpCorrigido = true; continue; }

                if (medicao.DataInicio.Hour < (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Cap)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(estagiosOrderAsc);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(estagiosOrderAsc, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }

                if (medicao.DataInicio.Hour >= (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Ind)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(estagiosOrderAsc);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(estagiosOrderAsc, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }
            }

            return medicoes;
        }

        public static List<Medicao> CompensacaoSequencialDescendente(
            List<Medicao> medicoes,
            List<EstagioResumo> estagios,
            double ck,
            double fpDesejado = Const.FP_0_92,
            double quantidadeEstagios = Const.QUANTIDADE_ESTAGIO_AUTOMATICO_DEFAULT)
        {
            var estagiosOrderDesc = BancoCapacitorUtil.OrdenarEstagiosPorPotencia(estagios, OrdenarPor.ModoDescendente);

            foreach (var medicao in medicoes)
            {
                if (medicao.FatorPotencia >= fpDesejado) { medicao.FpCorrigido = true; continue; }

                if (medicao.DataInicio.Hour < (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Cap)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(estagiosOrderDesc);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(estagiosOrderDesc, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }

                if (medicao.DataInicio.Hour >= (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Ind)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(estagiosOrderDesc);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(estagiosOrderDesc, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }
            }

            return medicoes;
        }

        public static List<Medicao> CompensacaoLinear(
            List<Medicao> medicoes,
            List<EstagioResumo> estagios,
            double ck,
            double fpDesejado = Const.FP_0_92,
            double quantidadeEstagios = Const.QUANTIDADE_ESTAGIO_AUTOMATICO_DEFAULT)
        {
            var listaEstagios = estagios.Select(e => e.Potencia).ToList();

            foreach (var medicao in medicoes)
            {
                if (medicao.FatorPotencia >= fpDesejado) { medicao.FpCorrigido = true; continue; }

                if (TemBaixoFatorPotenciaCapacitivoPelaMedicaoHoraria(medicao, fpDesejado))
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(listaEstagios);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(listaEstagios, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }

                if (TemBaixoFatorPotenciaIndutivoPelaMedicaoHoraria(medicao, fpDesejado))
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(listaEstagios);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(listaEstagios, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }
            }

            return medicoes;
        }

        public static bool TemBaixoFatorPotenciaCapacitivoPelaMedicaoHoraria(Medicao medicao, double fpDesejado)
        {
            return medicao.FatorPotencia < fpDesejado && medicao.DataInicio.Hour < (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Cap;
        }

        public static bool TemBaixoFatorPotenciaIndutivoPelaMedicaoHoraria(Medicao medicao, double fpDesejado)
        {
            return medicao.FatorPotencia < fpDesejado && medicao.DataInicio.Hour >= (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Ind;
        }


        public static List<Medicao> CompensacaoCircular(
            List<Medicao> medicoes,
            List<EstagioResumo> estagios,
            double ck,
            double fpDesejado = Const.FP_0_92,
            double quantidadeEstagios = Const.QUANTIDADE_ESTAGIO_AUTOMATICO_DEFAULT)
        {
            var listaEstagios = estagios.Select(e => e.Potencia).ToList();

            foreach (var medicao in medicoes)
            {
                if (medicao.FatorPotencia >= fpDesejado) { medicao.FpCorrigido = true; continue; }

                if (medicao.DataInicio.Hour < (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Cap)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(listaEstagios);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(listaEstagios, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }

                if (medicao.DataInicio.Hour >= (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Ind)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(listaEstagios);
                    var capacitoresUtilizados = BancoCapacitorUtil.UtilizarBancoCapacitores(listaEstagios, medicao.QcNecessario, quantidadeEstagios, medicao, ck);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }
            }

            return medicoes;
        }

        public static List<Medicao> CompensacaoModoInteligente(
            List<Medicao> medicoes,
            List<EstagioResumo> estagios,
            double ck,
            double fpDesejado = Const.FP_0_92,
            double quantidadeEstagios = Const.QUANTIDADE_ESTAGIO_AUTOMATICO_DEFAULT)
        {
            var estagiosOrderAsc = estagios
                .OrderBy(e => e.Potencia)
                .Select(e => e.Potencia)
                .ToList();

            double menorValorEstagio = 0;
            foreach (var menorValor in estagiosOrderAsc)
            {
                if (menorValor != 0) { menorValorEstagio = menorValor; break; }
            }

            foreach (var medicao in medicoes)
            {
                if (medicao.FatorPotencia >= fpDesejado) { medicao.FpCorrigido = true; continue; }

                if (medicao.DataInicio.Hour < (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Cap)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(estagiosOrderAsc);
                    var capacitoresUtilizados = BancoCapacitorUtil.MododInteligente(estagiosOrderAsc, medicao.QcNecessario, quantidadeEstagios, medicao);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }

                if (medicao.DataInicio.Hour >= (int)HoraEnum.Seis && medicao.TipoFatorPotencia == Const.Ind)
                {
                    medicao.QcNecessario = BancoCapacitorUtil.QcNecessario(medicao.PotenciaAtiva, medicao.FatorPotencia, fpDesejado);
                    double valorTotalBancoCapacitores = BancoCapacitorUtil.SomarCapacitores(estagiosOrderAsc);
                    var capacitoresUtilizados = BancoCapacitorUtil.MododInteligente(estagiosOrderAsc, medicao.QcNecessario, quantidadeEstagios, medicao);
                    BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);
                    medicao.TipoFatorPotenciaCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(medicao.PotenciaReativa, medicao.TipoFatorPotencia, medicao.EstagiosNecessarios.Sum());
                }
            }

            return medicoes;
        }

        public static List<double> MododInteligente(IList<double> estagios, double qcNecessario, double quantidadeEstagios, Medicao medicao)
        {
            if (estagios.Sum() < qcNecessario)
            {
                medicao.FpCorrigido = false;
                medicao.EstagiosNecessarios.AddRange(estagios.Where(valorEstagio => valorEstagio != 0));
                return medicao.EstagiosNecessarios;
            }

            var qtdEstagios = estagios.Count;
            var temp = new List<double>();
            temp.AddRange(estagios);
            double somatorioQc = 0;

            for (int count = 0; count < qtdEstagios; count++)
            {
                bool finalizado = false;
                var maiorValor = temp.Max();
                var contador = 0;

                foreach (var estagio in temp)
                {
                    contador++;

                    if (somatorioQc + estagio >= qcNecessario)
                    {
                        medicao.EstagiosNecessarios.Add(estagio);
                        finalizado = true;
                        break;
                    }

                    if (contador == temp.Count)
                    {
                        somatorioQc = somatorioQc + maiorValor;
                        medicao.EstagiosNecessarios.Add(maiorValor);
                    }
                }

                if (finalizado == true) { break; }
                temp.Remove(maiorValor);
            }

            return medicao.EstagiosNecessarios;
        }

        public static string VerificarTipoReativoFpCorrigido(double reativoMedicao, string TipoFpMedicao, double somaReativoEstagiosUtilizados)
        {
            if (somaReativoEstagiosUtilizados > reativoMedicao)
            {
                if (TipoFpMedicao == Const.Cap) return Const.Ind;
                if (TipoFpMedicao == Const.Ind) return Const.Cap;
            }

            return TipoFpMedicao;
        }

        public static double CalcularCk(double menorPotencia, double tensaoLinha, double relacaoTc)
        {
            var raizQuadradaDeTres = Math.Sqrt(3);
            var ck = (menorPotencia * Const.QuiloWatt) / (relacaoTc * raizQuadradaDeTres * tensaoLinha);
            return ck.ArredondarTresCasasDecimais();
        }

        public static List<double> OrdenarEstagiosPorPotencia(List<Business.Models.EstagioResumo> estagios, OrdenarPor ordenarPor)
        {
            var estagiosOrdenados = new List<double>();

            if (ordenarPor == OrdenarPor.ModoAscendente)
            {
                var listaEstagiosOrdenadosAscendente = estagios.OrderBy(e => e.Potencia).Select(a => a.Potencia).ToList();
                estagiosOrdenados.AddRange(listaEstagiosOrdenadosAscendente.Where(estagio => estagio != 0));
                return estagiosOrdenados;
            }

            var listaEstagiosOrdenadosDescendente = estagios.OrderByDescending(e => e.Potencia).Select(a => a.Potencia).ToList();
            estagiosOrdenados.AddRange(listaEstagiosOrdenadosDescendente.Where(estagio => estagio != 0));
            return estagiosOrdenados;
        }
    }
}
