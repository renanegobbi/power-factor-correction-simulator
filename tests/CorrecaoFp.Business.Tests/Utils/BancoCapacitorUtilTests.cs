using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Utils;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CorrecaoFp.Business.Tests.Utils
{
    public class BancoCapacitorUtilTests : BaseTests
    {
        readonly ITestOutputHelper _outputHelper;

        public BancoCapacitorUtilTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory(DisplayName = "Verifica Cálculo da Potênia Ativa com Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteCalcularPotenciaAtiva), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularPotenciaAtiva_DeveExecutarComSucesso(double potenciaReativa, double potenciaAparente, double potenciaAtiva, double potenciaAtivaArredondado)
        {
            var medicao = new Medicao { PotenciaReativa = potenciaReativa, PotenciaAparente = potenciaAparente };

            var valorPotenciaAtivaCalculado = BancoCapacitorUtil.CalcularPotenciaAtiva(medicao);

            var resultado = valorPotenciaAtivaCalculado.Equals(potenciaAtivaArredondado);

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Valor de potência ativa calculado: {valorPotenciaAtivaCalculado} - " +
                $"Valor de potência ativa excel: {potenciaAtivaArredondado}");

            resultado.Should().BeTrue();
        }

        [Theory(DisplayName = "Verifica Cálculo da Potênia Ativa sem Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteCalcularPotenciaAtiva), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularPotenciaAtiva_DeveExecutarSemSucesso(double potenciaReativa, double potenciaAparente, double potenciaAtiva, double potenciaAtivaArredondado)
        {
            var medicao = new Medicao { PotenciaReativa = potenciaReativa, PotenciaAparente = potenciaAparente };

            var valorPotenciaAtivaCalculado = BancoCapacitorUtil.CalcularPotenciaAtiva(medicao);

            var resultado = valorPotenciaAtivaCalculado.Equals(potenciaAtiva);

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Valor de potência ativa calculado: {valorPotenciaAtivaCalculado} - " +
                $"Valor de potência ativa excel: {potenciaAtiva}");

            resultado.Should().BeFalse();
        }

        [Theory(DisplayName = "Verifica Cálculo do C/k com Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteCalcularCk), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularCk_DeveExecutarComSucesso(double menorPotencia, double tensaoLinha, double relacaoTc, double valorCkArredondado)
        {
            var valorCkCalculado = BancoCapacitorUtil.CalcularCk(menorPotencia, tensaoLinha, relacaoTc);

            var resultado = valorCkCalculado.Equals(valorCkArredondado);

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Valor de c/k calculado: {valorCkCalculado} - " +
                $"Valor de c/k excel: {valorCkArredondado}");

            resultado.Should().BeTrue();
        }

        [Theory(DisplayName = "Verifica Cálculo do C/k sem Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteCalcularCk), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularCk_DeveExecutarSemSucesso(double menorPotencia, double tensaoLinha, double relacaoTc, double valorCkArredondado)
        {
            var valorCkCalculado = BancoCapacitorUtil.CalcularCk(menorPotencia, tensaoLinha, relacaoTc);

            var resultado = valorCkCalculado.Equals(menorPotencia);

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Valor de c/k calculado: {valorCkCalculado} - " +
                $"Valor de c/k igual menor potência excel: {menorPotencia}");

            resultado.Should().BeFalse();
        }

        [Theory(DisplayName = "Verifica Cálculo do QcNecessario com Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteSomaCapacitores), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularSomaCapacitores_DeveExecutarComSucesso(
            double Capacitor_1, double Capacitor_2, double Capacitor_3, 
            double Capacitor_4, double Capacitor_5, double Capacitor_6, 
            double Capacitor_7, double Capacitor_8, double Capacitor_9, 
            double Capacitor_10, double Capacitor_11, double Capacitor_12, double Soma)
        {

            var estagios = new List<double>
            {
                Capacitor_1,
                Capacitor_2,
                Capacitor_3,
                Capacitor_4,
                Capacitor_5,
                Capacitor_6,
                Capacitor_7,
                Capacitor_8,
                Capacitor_9,
                Capacitor_10,
                Capacitor_11,
                Capacitor_12
            };
            
            var somaCapacitoresCalculado = BancoCapacitorUtil.SomarCapacitores(estagios);

            var resultado = somaCapacitoresCalculado == Soma ;

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Soma capcitores calculado: {somaCapacitoresCalculado} - " +
                $"Soma capcitores excel: {Soma}");

            resultado.Should().BeTrue();
        }

        [Theory(DisplayName = "Verifica Cálculo do QcNecessario sem Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteSomaCapacitores), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularSomaCapacitores_DeveExecutarSemSucesso(
            double Capacitor_1, double Capacitor_2, double Capacitor_3,
            double Capacitor_4, double Capacitor_5, double Capacitor_6,
            double Capacitor_7, double Capacitor_8, double Capacitor_9,
            double Capacitor_10, double Capacitor_11, double Capacitor_12, double Soma)
        {
            Soma = 0;

            var estagios = new List<double>
            {
                Capacitor_1,
                Capacitor_2,
                Capacitor_3,
                Capacitor_4,
                Capacitor_5,
                Capacitor_6,
                Capacitor_7,
                Capacitor_8,
                Capacitor_9,
                Capacitor_10,
                Capacitor_11,
                Capacitor_12
            };

            var somaCapacitoresCalculado = BancoCapacitorUtil.SomarCapacitores(estagios);

            var resultado = somaCapacitoresCalculado == Soma;

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Soma capcitores calculado: {somaCapacitoresCalculado} - " +
                $"Soma capcitores excel: {Soma}");

            resultado.Should().BeFalse();
        }

        [Theory(DisplayName = "Verifica Cálculo do QcNecessario com Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteQcNecessario), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularQcNecessario_DeveExecutarComSucesso(
            double PotenciaAtiva, 
            double FpInicial, 
            double FpDesejado, 
            double PotenciaCapacitivaNecessario,
            double PotenciaCapacitivaNecessarioArredondado)
        {
            var qcNecessarioCalculado = BancoCapacitorUtil.QcNecessario(PotenciaAtiva, FpInicial, FpDesejado);

            var resultado = qcNecessarioCalculado == PotenciaCapacitivaNecessarioArredondado;

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"QcNecessário calculado: {qcNecessarioCalculado} - " +
                $"QcNecessário excel: {PotenciaCapacitivaNecessarioArredondado}");

            resultado.Should().BeTrue();
        }

        [Theory(DisplayName = "Verifica Cálculo do QcNecessario sem Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteQcNecessario), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_CalcularQcNecessario_DeveExecutarSemSucesso(double PotenciaAtiva, double FpInicial, double FpDesejado, double PotenciaCapacitivaNecessario, double PotenciaCapacitivaNecessarioArredondado)
        {
            var qcNecessarioCalculado = BancoCapacitorUtil.QcNecessario(PotenciaAtiva, FpInicial, FpDesejado);

            var resultado = qcNecessarioCalculado == PotenciaCapacitivaNecessario;

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"QcNecessário calculado: {qcNecessarioCalculado} - " +
                $"QcNecessário excel: {PotenciaCapacitivaNecessario}");

            resultado.Should().BeFalse();
        }

        [Theory(DisplayName = "Verifica Recalcular Fp com Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteRecalcularFp), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_RecalcularFp_DeveExecutarComSucesso(double PotenciaAtiva, double TotalPotenciaReativaAdicionada, double FpInicial, double FpFinal, double FpFinalArredondado)
        {
            var medicao = new Medicao();
            medicao.PotenciaAtiva = PotenciaAtiva;
            medicao.FatorPotencia = FpInicial;

            var capacitoresUtilizados = new List<double>{ TotalPotenciaReativaAdicionada };

            var fpFinalCalculado = BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);

            var resultado = fpFinalCalculado == FpFinalArredondado;

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"FpFinal calculado: {fpFinalCalculado} - " +
                $"FpFinal excel: {FpFinal}");

            resultado.Should().BeTrue();
        }

        [Theory(DisplayName = "Verifica Recalcular Fp sem Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteRecalcularFp), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_RecalcularFp_DeveExecutarSemSucesso(double PotenciaAtiva, double TotalPotenciaReativaAdicionada, double FpInicial, double FpFinal, double FpFinalArredondado)
        {
            var medicao = new Medicao();
            medicao.PotenciaAtiva = PotenciaAtiva;
            medicao.FatorPotencia = FpInicial;

            var capacitoresUtilizados = new List<double> { TotalPotenciaReativaAdicionada };

            var fpFinalCalculado = BancoCapacitorUtil.RecalcularFp(capacitoresUtilizados, medicao);

            var resultado = fpFinalCalculado == FpFinal;

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"FpFinal calculado: {fpFinalCalculado} - " +
                $"FpFinal excel: {FpFinal}");

            resultado.Should().BeFalse();
        }

        [Theory(DisplayName = "Verifica Ordenação do Estágios por Potência com Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteOrdenarEstagioPorPotencia), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_OrdenarEstagiosPorPotencia_DeveExecutarComSucesso(
            List<double> listaPotenciaCapacitor, OrdenarPor ordenarPor, List<double> listaCapacitoresOrdenado)
        {

            var listaEstagioResumo = new List<EstagioResumo>();
            foreach (var potenciaCapacitor in listaPotenciaCapacitor)
            {
                var estagioResumo = new EstagioResumo();
                estagioResumo.Potencia = potenciaCapacitor;
                listaEstagioResumo.Add(estagioResumo);
            }

            var capacitoresOrdenadosPorCodigo = BancoCapacitorUtil.OrdenarEstagiosPorPotencia(listaEstagioResumo, ordenarPor);

            var resultado = true;
            var comparacao = true;

            for (int posicao = 0; posicao < listaEstagioResumo.Count(); posicao++)
            {
                comparacao = capacitoresOrdenadosPorCodigo[posicao] == listaCapacitoresOrdenado[posicao];

                if (comparacao == false)
                {
                    resultado = false;
                    break;
                }
            }
        }

        [Theory(DisplayName = "Verifica Tipo Reativo do FP Corrigido com Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteVerificarTipoReativoFpCorrigido), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_VerificarTipoReativoFpCorrigido_DeveExecutarComSucesso(double potenciaReativa, string tipoFp, double somaReativoEstagiosUtilizados, string tipoFpCorrigido)
        {
            var valorTipoFpCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(potenciaReativa, tipoFp, somaReativoEstagiosUtilizados);

            var resultado = valorTipoFpCorrigido.Equals(tipoFpCorrigido);

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Valor do tipo de fator de potência corrigido verificado: {valorTipoFpCorrigido} - " +
                $"Valor do tipo de fator de potência corrigido excel: {tipoFpCorrigido}");

            resultado.Should().BeTrue();
        }

        [Theory(DisplayName = "Verifica Tipo Reativo do FP Corrigido sem Sucesso")]
        [Trait("Categoria", "Banco de Capacitor Util Tests")]
        [MemberData(nameof(CargaDadosTeste.ListaTesteVerificarTipoReativoFpCorrigido), MemberType = typeof(CargaDadosTeste))]
        public void BancoCapacitorUtil_VerificarTipoReativoFpCorrigido_DeveExecutarSemSucesso(double potenciaReativa, string tipoFp, double somaReativoEstagiosUtilizados, string tipoFpCorrigido)
        {
            tipoFpCorrigido = "";

            var valorTipoFpCorrigido = BancoCapacitorUtil.VerificarTipoReativoFpCorrigido(potenciaReativa, tipoFp, somaReativoEstagiosUtilizados);

            var resultado = valorTipoFpCorrigido.Equals(tipoFpCorrigido);

            _outputHelper.WriteLine($"Resultado: {resultado} - " +
                $"Valor do tipo de fator de potência corrigido verificado: {valorTipoFpCorrigido} - " +
                $"Valor do tipo de fator de potência corrigido excel: {tipoFpCorrigido}");

            resultado.Should().BeFalse();
        }

    }
}