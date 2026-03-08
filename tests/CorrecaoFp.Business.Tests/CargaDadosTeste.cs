using CorrecaoFp.Business.Enums;
using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Tests.File;
using CorrecaoFp.Business.Tests.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace CorrecaoFp.Business.Tests
{
    public static class CargaDadosTeste
    {
        public static IList<QcNecessario> ListQcNecessario { get; set; }
        public static IList<SomaCapacitores> ListaSomaCapacitores { get; set; }
        public static IList<RecalcularFp> ListaRecalcularFp { get; set; }
        public static IList<OrdenarEstagioPorPotencia> ListaOrdenarEstagiosPorPotencia { get; set; }
        public static IList<CalcularCk> ListaCalcularCk { get; set; }
        public static IList<CalcularPotenciaAtiva> ListaCalcularPotenciaAtiva { get; set; }
        public static IList<VerificarTipoReativoFpCorrigido> ListaVerificarTipoReativoFpCorrigido { get; set; }

        static CargaDadosTeste()
        {
            ListaCalcularPotenciaAtiva = FileUtil.LerPlanilhaCalcularPotenciaAtiva(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
            ListQcNecessario = FileUtil.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
            ListaSomaCapacitores = FileUtil.LerPlanilhaSomarCapacitores(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
            ListaRecalcularFp = FileUtil.LerPlanilhaRecalcularFp(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
            ListaOrdenarEstagiosPorPotencia = FileUtil.LerPlanilhaOrdenarEstagiosPorPotencia(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
            ListaCalcularCk = FileUtil.LerPlanilhaCalcularCk(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
            ListaVerificarTipoReativoFpCorrigido = FileUtil.LerPlanilhaVerificarTipoReativoFpCorrigido(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
        }

        public static IEnumerable<object[]> ListaTesteCalcularPotenciaAtiva()
        {
            List<Object[]> objectList = new List<Object[]>();

            foreach (var item in ListaCalcularPotenciaAtiva)
            {
                objectList.Add(new object[] {
                    item.PotenciaReativa,
                    item.PotenciaAparente,
                    item.PotenciaAtiva,
                    item.PotenciaAtivaArredondado
                });
            }

            return objectList;
        }

        public static IEnumerable<object[]> ListaTesteQcNecessario()
        {
            List<Object[]> objectList = new List<Object[]>();

            foreach (var item in ListQcNecessario)
            {
                objectList.Add(new object[] {
                    item.PotenciaAtiva, 
                    item.FpInicial, 
                    item.FpDesejado, 
                    item.PotenciaCapacitivaNecessario, 
                    item.PotenciaCapacitivaNecessarioArredondado 
                });
            }

            return objectList;
        }

        public static IEnumerable<object[]> ListaTesteSomaCapacitores()
        {
            List<Object[]> objectList = new List<Object[]>();

            foreach (var item in ListaSomaCapacitores)
            {
                objectList.Add(new object[] {
                    item.Capacitor_1,
                    item.Capacitor_2,
                    item.Capacitor_3,
                    item.Capacitor_4,
                    item.Capacitor_5,
                    item.Capacitor_6,
                    item.Capacitor_7,
                    item.Capacitor_8,
                    item.Capacitor_9,
                    item.Capacitor_10,
                    item.Capacitor_11,
                    item.Capacitor_12,
                    item.Soma
                });
            }

            return objectList;
        }

        public static IEnumerable<object[]> ListaTesteRecalcularFp()
        {
            List<Object[]> objectList = new List<Object[]>();

            foreach (var item in ListaRecalcularFp)
            {
                objectList.Add(new object[] {
                    item.PotenciaAtiva,
                    item.PotenciaReativaNecessaria,
                    item.FpInicial,
                    item.FpFinal,
                    item.FpFinalArredondado
                });
            }

            return objectList;
        }

        public static IEnumerable<object[]> ListaTesteOrdenarEstagioPorPotencia()
        {
            List<Object[]> objectList = new List<Object[]>();

            List<double> listaCapacitores;
            List<double> listaCapacitoresOrdenado;

            foreach (var item in ListaOrdenarEstagiosPorPotencia)
            {
                listaCapacitores = new List<double>() {
                    item.Capacitor_1,
                    item.Capacitor_2,
                    item.Capacitor_3,
                    item.Capacitor_4,
                    item.Capacitor_5,
                    item.Capacitor_6,
                    item.Capacitor_7,
                    item.Capacitor_8,
                    item.Capacitor_9,
                    item.Capacitor_10,
                    item.Capacitor_11,
                    item.Capacitor_12
                };

                var ordenarPor = item.OrdenarPor == "Ascendente"
                    ? OrdenarPor.ModoAscendente
                    : OrdenarPor.ModoDescendente;

                listaCapacitoresOrdenado = new List<double>() {
                    item.Capacitor_1_1,
                    item.Capacitor_2_1,
                    item.Capacitor_3_1,
                    item.Capacitor_4_1,
                    item.Capacitor_5_1,
                    item.Capacitor_6_1,
                    item.Capacitor_7_1,
                    item.Capacitor_8_1,
                    item.Capacitor_9_1,
                    item.Capacitor_10_1,
                    item.Capacitor_11_1,
                    item.Capacitor_12_1
                };

                objectList.Add(new object[] {
                    listaCapacitores,
                    ordenarPor,
                    listaCapacitoresOrdenado
                });
            }

            return objectList;
        }

        public static IEnumerable<object[]> ListaTesteCalcularCk()
        {
            List<Object[]> objectList = new List<Object[]>();

            foreach (var item in ListaCalcularCk)
            {
                objectList.Add(new object[] {
                    item.MenorPotencia,
                    item.TensaoLinha,
                    item.RelacaoTc,
                    item.ValorCkArredondado
                });
            }

            return objectList;
        }

        public static IEnumerable<object[]> ListaTesteVerificarTipoReativoFpCorrigido()
        {
            List<Object[]> objectList = new List<Object[]>();

            foreach (var item in ListaVerificarTipoReativoFpCorrigido)
            {
                objectList.Add(new object[] {
                    item.PotenciaReativa,
                    item.TipoFp,
                    item.SomaReativoEstagiosUtilizados,
                    item.TipoFpCorrigido
                });
            }

            return objectList;
        }
    }
}