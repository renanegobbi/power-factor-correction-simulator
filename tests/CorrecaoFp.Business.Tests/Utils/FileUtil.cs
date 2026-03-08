using CorrecaoFp.Business.Models;
using CorrecaoFp.Business.Tests.File;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CorrecaoFp.Business.Tests.Utils
{
    public static class FileUtil
    {
        public const string ExtensaoXlsx = ".xlsx";
        public const long TamanhoArquivoBytes = 2147483648; // 2 gigabytes = 2147483648 bytes
        public const long TamanhoArquivoGigabytes = 4;

        public static FileAux CriarFileAux()
        {
            return new FileAux();
        }

        public static bool ExtensaoValida(IFormFile ArquivoUpload)
        {
            var extensao = Path.GetExtension(ArquivoUpload.FileName).ToLower();

            var verifyExtension = extensao.Equals(ExtensaoXlsx) ? true : false;

            return extensao.Equals(ExtensaoXlsx) ? true : false;
        }

        public static string ExtensaoArquivo(IFormFile ArquivoUpload)
        {
            var extensao = Path.GetExtension(ArquivoUpload.FileName);

            return extensao;
        }

        public static bool TamanhoValido(IFormFile ArquivoUpload)
        {
            return ArquivoUpload.Length <= TamanhoArquivoBytes;
        }

        public static List<QcNecessario> ReadFile(string path)
        {
            var listQcNecessario = new List<QcNecessario>();

            FileInfo existingFile = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["QcNecessario"];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                var potenciaAtiva = worksheet.Cells[1, 1].Value;
                var fpInicial = worksheet.Cells[1, 2].Value;
                var fpDesejado = worksheet.Cells[1, 3].Value;
                var qcNecessario = worksheet.Cells[1, 4].Value;
                var qcNecessarioArredondado = worksheet.Cells[1, 5].Value;

                if (!potenciaAtiva.Equals("Potência Ativa (kW)") ||
                    !fpInicial.Equals("FPinicial") ||
                    !fpDesejado.Equals("FPdesejado") ||
                    !qcNecessario.Equals("QcNecessario (kVAr)") ||
                    !qcNecessarioArredondado.Equals("QcNecessário (arrendodado 2 casas decimais)")
                    )
                {
                    //Validar
                    return null;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaPotenciaAtiva = worksheet.Cells[row, 1].Value;
                    var colunaFpInicial = worksheet.Cells[row, 2].Value;
                    var colunaFpDesejado = worksheet.Cells[row, 3].Value;
                    var colunaQcNcessario = worksheet.Cells[row, 4].Value;
                    var colunaQcNecessarioArredondado = worksheet.Cells[row, 5].Value;

                    var qcNecessarioLinha = new QcNecessario();

                    qcNecessarioLinha.PotenciaAtiva = (Convert.ToDouble(colunaPotenciaAtiva));
                    qcNecessarioLinha.FpInicial = (Convert.ToDouble(colunaFpInicial));
                    qcNecessarioLinha.FpDesejado = (Convert.ToDouble(colunaFpDesejado));
                    qcNecessarioLinha.PotenciaCapacitivaNecessario = (Convert.ToDouble(colunaQcNcessario));
                    qcNecessarioLinha.PotenciaCapacitivaNecessarioArredondado = (Convert.ToDouble(colunaQcNecessarioArredondado));

                    listQcNecessario.Add(qcNecessarioLinha);
                }
            }

            return listQcNecessario;
        }

        public static List<SomaCapacitores> LerPlanilhaSomarCapacitores(string path)
        {
            var listaSomaCapacitores = new List<SomaCapacitores>();

            FileInfo existingFile = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["SomarCapacitores"];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                var capacitor_1 = worksheet.Cells[1, 1].Value;
                var capacitor_2 = worksheet.Cells[1, 2].Value;
                var capacitor_3 = worksheet.Cells[1, 3].Value;
                var capacitor_4 = worksheet.Cells[1, 4].Value;
                var capacitor_5 = worksheet.Cells[1, 5].Value;
                var capacitor_6 = worksheet.Cells[1, 6].Value;
                var capacitor_7 = worksheet.Cells[1, 7].Value;
                var capacitor_8 = worksheet.Cells[1, 8].Value;
                var capacitor_9 = worksheet.Cells[1, 9].Value;
                var capacitor_10 = worksheet.Cells[1, 10].Value;
                var capacitor_11 = worksheet.Cells[1, 11].Value;
                var capacitor_12 = worksheet.Cells[1, 12].Value;
                var soma = worksheet.Cells[1, 13].Value;

                if (!capacitor_1.Equals("Capacitor_1 (Kw)") ||
                    !capacitor_2.Equals("Capacitor_2 (Kw)") ||
                    !capacitor_3.Equals("Capacitor_3 (Kw)") ||
                    !capacitor_4.Equals("Capacitor_4 (Kw)") ||
                    !capacitor_5.Equals("Capacitor_5 (Kw)") ||
                    !capacitor_6.Equals("Capacitor_6 (Kw)") ||
                    !capacitor_7.Equals("Capacitor_7 (Kw)") ||
                    !capacitor_8.Equals("Capacitor_8 (Kw)") ||
                    !capacitor_9.Equals("Capacitor_9 (Kw)") ||
                    !capacitor_10.Equals("Capacitor_10 (Kw)") ||
                    !capacitor_11.Equals("Capacitor_11 (Kw)") ||
                    !capacitor_12.Equals("Capacitor_12 (Kw)") ||
                    !soma.Equals("Soma")
                    )
                {
                    //Validar
                    return null;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaCapacitor_1 = worksheet.Cells[row, 1].Value;
                    var colunaCapacitor_2 = worksheet.Cells[row, 2].Value;
                    var colunaCapacitor_3 = worksheet.Cells[row, 3].Value;
                    var colunaCapacitor_4 = worksheet.Cells[row, 4].Value;
                    var colunaCapacitor_5 = worksheet.Cells[row, 5].Value;
                    var colunaCapacitor_6 = worksheet.Cells[row, 6].Value;
                    var colunaCapacitor_7 = worksheet.Cells[row, 7].Value;
                    var colunaCapacitor_8 = worksheet.Cells[row, 8].Value;
                    var colunaCapacitor_9 = worksheet.Cells[row, 9].Value;
                    var colunaCapacitor_10 = worksheet.Cells[row, 10].Value;
                    var colunaCapacitor_11 = worksheet.Cells[row, 11].Value;
                    var colunaCapacitor_12 = worksheet.Cells[row, 12].Value;
                    var colunaSoma = worksheet.Cells[row, 13].Value;

                    var linhaSomaCapacitores = new SomaCapacitores();

                    linhaSomaCapacitores.Capacitor_1 = (Convert.ToDouble(colunaCapacitor_1));
                    linhaSomaCapacitores.Capacitor_2 = (Convert.ToDouble(colunaCapacitor_2));
                    linhaSomaCapacitores.Capacitor_3 = (Convert.ToDouble(colunaCapacitor_3));
                    linhaSomaCapacitores.Capacitor_4 = (Convert.ToDouble(colunaCapacitor_4));
                    linhaSomaCapacitores.Capacitor_5 = (Convert.ToDouble(colunaCapacitor_5));
                    linhaSomaCapacitores.Capacitor_6 = (Convert.ToDouble(colunaCapacitor_6));
                    linhaSomaCapacitores.Capacitor_7 = (Convert.ToDouble(colunaCapacitor_7));
                    linhaSomaCapacitores.Capacitor_8 = (Convert.ToDouble(colunaCapacitor_8));
                    linhaSomaCapacitores.Capacitor_9 = (Convert.ToDouble(colunaCapacitor_9));
                    linhaSomaCapacitores.Capacitor_10 = (Convert.ToDouble(colunaCapacitor_10));
                    linhaSomaCapacitores.Capacitor_11 = (Convert.ToDouble(colunaCapacitor_11));
                    linhaSomaCapacitores.Capacitor_12 = (Convert.ToDouble(colunaCapacitor_12));
                    linhaSomaCapacitores.Soma = (Convert.ToDouble(colunaSoma));

                    listaSomaCapacitores.Add(linhaSomaCapacitores);
                }
            }

            return listaSomaCapacitores;
        }

        public static List<RecalcularFp> LerPlanilhaRecalcularFp(string path)
        {
            var listaRecalcularFp = new List<RecalcularFp>();

            FileInfo existingFile = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["RecalcularFp"];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                var potenciaAtiva = worksheet.Cells[1, 1].Value;
                var potenciaReativaNecessaria = worksheet.Cells[1, 2].Value;
                var fpInicial = worksheet.Cells[1, 3].Value;
                var fpFinal = worksheet.Cells[1, 4].Value;
                var fpFinalArredondado = worksheet.Cells[1, 5].Value;

                if (!potenciaAtiva.Equals("PotenciaAtiva") ||
                    !potenciaReativaNecessaria.Equals("PotenciaReativaNecessaria") ||
                    !fpInicial.Equals("FpInicial") ||
                    !fpFinal.Equals("FpFinal") ||
                    !fpFinalArredondado.Equals("FpFinalArredondado")
                    )
                {
                    //Validar
                    return null;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaPotenciaAtiva = worksheet.Cells[row, 1].Value;
                    var colunaPotenciaReativaNecessaria = worksheet.Cells[row, 2].Value;
                    var colunaFpInicial = worksheet.Cells[row, 3].Value;
                    var colunaFpFina = worksheet.Cells[row, 4].Value;
                    var colunaFpFinalArredondado = worksheet.Cells[row, 5].Value;

                    if (colunaPotenciaAtiva == null) { continue; }

                    var linhaRecalcularFp = new RecalcularFp();

                    linhaRecalcularFp.PotenciaAtiva = (Convert.ToDouble(colunaPotenciaAtiva));
                    linhaRecalcularFp.PotenciaReativaNecessaria = (Convert.ToDouble(colunaPotenciaReativaNecessaria));
                    linhaRecalcularFp.FpInicial = (Convert.ToDouble(colunaFpInicial));
                    linhaRecalcularFp.FpFinal = (Convert.ToDouble(colunaFpFina));
                    linhaRecalcularFp.FpFinalArredondado = (Convert.ToDouble(colunaFpFinalArredondado));

                    listaRecalcularFp.Add(linhaRecalcularFp);
                }
            }

            return listaRecalcularFp;
        }

        public static List<OrdenarEstagioPorPotencia> LerPlanilhaOrdenarEstagiosPorPotencia(string path)
        {
            var listaOrdenarEstagioPorPotencia = new List<OrdenarEstagioPorPotencia>();

            FileInfo existingFile = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["OrdenarEstagiosPorPotencia"];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                var capacitor_1 = worksheet.Cells[1, 1].Value;
                var capacitor_2 = worksheet.Cells[1, 2].Value;
                var capacitor_3 = worksheet.Cells[1, 3].Value;
                var capacitor_4 = worksheet.Cells[1, 4].Value;
                var capacitor_5 = worksheet.Cells[1, 5].Value;
                var capacitor_6 = worksheet.Cells[1, 6].Value;
                var capacitor_7 = worksheet.Cells[1, 7].Value;
                var capacitor_8 = worksheet.Cells[1, 8].Value;
                var capacitor_9 = worksheet.Cells[1, 9].Value;
                var capacitor_10 = worksheet.Cells[1, 10].Value;
                var capacitor_11 = worksheet.Cells[1, 11].Value;
                var capacitor_12 = worksheet.Cells[1, 12].Value;
                var ordenarPor = worksheet.Cells[1, 13].Value;
                var capacitor_1_1 = worksheet.Cells[1, 14].Value;
                var capacitor_2_1 = worksheet.Cells[1, 15].Value;
                var capacitor_3_1 = worksheet.Cells[1, 16].Value;
                var capacitor_4_1 = worksheet.Cells[1, 17].Value;
                var capacitor_5_1 = worksheet.Cells[1, 18].Value;
                var capacitor_6_1 = worksheet.Cells[1, 19].Value;
                var capacitor_7_1 = worksheet.Cells[1, 20].Value;
                var capacitor_8_1 = worksheet.Cells[1, 21].Value;
                var capacitor_9_1 = worksheet.Cells[1, 22].Value;
                var capacitor_10_1 = worksheet.Cells[1, 23].Value;
                var capacitor_11_1 = worksheet.Cells[1, 24].Value;
                var capacitor_12_1 = worksheet.Cells[1, 25].Value;


                if (!capacitor_1.Equals("Capacitor_1 (Kw)") ||
                    !capacitor_2.Equals("Capacitor_2 (Kw)") ||
                    !capacitor_3.Equals("Capacitor_3 (Kw)") ||
                    !capacitor_4.Equals("Capacitor_4 (Kw)") ||
                    !capacitor_5.Equals("Capacitor_5 (Kw)") ||
                    !capacitor_6.Equals("Capacitor_6 (Kw)") ||
                    !capacitor_7.Equals("Capacitor_7 (Kw)") ||
                    !capacitor_8.Equals("Capacitor_8 (Kw)") ||
                    !capacitor_9.Equals("Capacitor_9 (Kw)") ||
                    !capacitor_10.Equals("Capacitor_10 (Kw)") ||
                    !capacitor_11.Equals("Capacitor_11 (Kw)") ||
                    !capacitor_12.Equals("Capacitor_12 (Kw)") ||
                    !capacitor_12.Equals("Capacitor_12 (Kw)") ||
                    !ordenarPor.Equals("OrdenarPor") ||
                    !capacitor_1_1.Equals("Capacitor_1_1 (Kw)") ||
                    !capacitor_2_1.Equals("Capacitor_2_1 (Kw)") ||
                    !capacitor_3_1.Equals("Capacitor_3_1 (Kw)") ||
                    !capacitor_4_1.Equals("Capacitor_4_1 (Kw)") ||
                    !capacitor_5_1.Equals("Capacitor_5_1 (Kw)") ||
                    !capacitor_6_1.Equals("Capacitor_6_1 (Kw)") ||
                    !capacitor_7_1.Equals("Capacitor_7_1 (Kw)") ||
                    !capacitor_8_1.Equals("Capacitor_8_1 (Kw)") ||
                    !capacitor_9_1.Equals("Capacitor_9_1 (Kw)") ||
                    !capacitor_10_1.Equals("Capacitor_10_1 (Kw)") ||
                    !capacitor_11_1.Equals("Capacitor_11_1 (Kw)") ||
                    !capacitor_12_1.Equals("Capacitor_12_1 (Kw)") 
                    )
                {
                    //Validar
                    return null;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaCapacitor_1 = worksheet.Cells[row, 1].Value;
                    var colunaCapacitor_2 = worksheet.Cells[row, 2].Value;
                    var colunaCapacitor_3 = worksheet.Cells[row, 3].Value;
                    var colunaCapacitor_4 = worksheet.Cells[row, 4].Value;
                    var colunaCapacitor_5 = worksheet.Cells[row, 5].Value;
                    var colunaCapacitor_6 = worksheet.Cells[row, 6].Value;
                    var colunaCapacitor_7 = worksheet.Cells[row, 7].Value;
                    var colunaCapacitor_8 = worksheet.Cells[row, 8].Value;
                    var colunaCapacitor_9 = worksheet.Cells[row, 9].Value;
                    var colunaCapacitor_10 = worksheet.Cells[row, 10].Value;
                    var colunaCapacitor_11 = worksheet.Cells[row, 11].Value;
                    var colunaCapacitor_12 = worksheet.Cells[row, 12].Value;
                    var colunaOrdenarPor = worksheet.Cells[row, 13].Value;
                    var colunaCapacitor_1_1 = worksheet.Cells[row, 14].Value;
                    var colunaCapacitor_2_1 = worksheet.Cells[row, 15].Value;
                    var colunaCapacitor_3_1 = worksheet.Cells[row, 16].Value;
                    var colunaCapacitor_4_1 = worksheet.Cells[row, 17].Value;
                    var colunaCapacitor_5_1 = worksheet.Cells[row, 18].Value;
                    var colunaCapacitor_6_1 = worksheet.Cells[row, 19].Value;
                    var colunaCapacitor_7_1 = worksheet.Cells[row, 20].Value;
                    var colunaCapacitor_8_1 = worksheet.Cells[row, 21].Value;
                    var colunaCapacitor_9_1 = worksheet.Cells[row, 22].Value;
                    var colunaCapacitor_10_1 = worksheet.Cells[row, 23].Value;
                    var colunaCapacitor_11_1 = worksheet.Cells[row, 24].Value;
                    var colunaCapacitor_12_1 = worksheet.Cells[row, 25].Value;

                    var linhaOrdenarEstagioPorPotencia = new OrdenarEstagioPorPotencia();

                    linhaOrdenarEstagioPorPotencia.Capacitor_1 = (Convert.ToDouble(colunaCapacitor_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_2 = (Convert.ToDouble(colunaCapacitor_2));
                    linhaOrdenarEstagioPorPotencia.Capacitor_3 = (Convert.ToDouble(colunaCapacitor_3));
                    linhaOrdenarEstagioPorPotencia.Capacitor_4 = (Convert.ToDouble(colunaCapacitor_4));
                    linhaOrdenarEstagioPorPotencia.Capacitor_5 = (Convert.ToDouble(colunaCapacitor_5));
                    linhaOrdenarEstagioPorPotencia.Capacitor_6 = (Convert.ToDouble(colunaCapacitor_6));
                    linhaOrdenarEstagioPorPotencia.Capacitor_7 = (Convert.ToDouble(colunaCapacitor_7));
                    linhaOrdenarEstagioPorPotencia.Capacitor_8 = (Convert.ToDouble(colunaCapacitor_8));
                    linhaOrdenarEstagioPorPotencia.Capacitor_9 = (Convert.ToDouble(colunaCapacitor_9));
                    linhaOrdenarEstagioPorPotencia.Capacitor_10 = (Convert.ToDouble(colunaCapacitor_10));
                    linhaOrdenarEstagioPorPotencia.Capacitor_11 = (Convert.ToDouble(colunaCapacitor_11));
                    linhaOrdenarEstagioPorPotencia.Capacitor_12 = (Convert.ToDouble(colunaCapacitor_12));
                    linhaOrdenarEstagioPorPotencia.OrdenarPor = ((colunaOrdenarPor).ToString());
                    linhaOrdenarEstagioPorPotencia.Capacitor_1_1 = (Convert.ToDouble(colunaCapacitor_1_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_2_1 = (Convert.ToDouble(colunaCapacitor_2_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_3_1 = (Convert.ToDouble(colunaCapacitor_3_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_4_1 = (Convert.ToDouble(colunaCapacitor_4_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_5_1 = (Convert.ToDouble(colunaCapacitor_5_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_6_1 = (Convert.ToDouble(colunaCapacitor_6_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_7_1 = (Convert.ToDouble(colunaCapacitor_7_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_8_1 = (Convert.ToDouble(colunaCapacitor_8_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_9_1 = (Convert.ToDouble(colunaCapacitor_9_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_10_1 = (Convert.ToDouble(colunaCapacitor_10_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_11_1 = (Convert.ToDouble(colunaCapacitor_11_1));
                    linhaOrdenarEstagioPorPotencia.Capacitor_12_1 = (Convert.ToDouble(colunaCapacitor_12_1));

                    listaOrdenarEstagioPorPotencia.Add(linhaOrdenarEstagioPorPotencia);
                }
            }

            return listaOrdenarEstagioPorPotencia;
        }

        public static List<CalcularCk> LerPlanilhaCalcularCk(string path)
        {
            var listaCalcularFp = new List<CalcularCk>();

            FileInfo existingFile = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["CalcularCk"];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                var menorPotencia = worksheet.Cells[1, 1].Value;
                var tensaoLinha = worksheet.Cells[1, 2].Value;
                var relacaoTc = worksheet.Cells[1, 3].Value;
                var valorCk = worksheet.Cells[1, 4].Value;
                var valorCkArredondado = worksheet.Cells[1, 5].Value;

                if (!menorPotencia.Equals("MenorPotencia (kVAr)") ||
                    !tensaoLinha.Equals("TensaoLinha (V)") ||
                    !relacaoTc.Equals("RelacaoTc") ||
                    !valorCk.Equals("C/k") ||
                    !valorCkArredondado.Equals("C/k (arrendodado 3 casas decimais)")
                    )
                {
                    //Validar
                    return null;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaMenorPotencia = worksheet.Cells[row, 1].Value;
                    var colunaTensaoLinha = worksheet.Cells[row, 2].Value;
                    var colunaRelacaoTc = worksheet.Cells[row, 3].Value;
                    var colunaCk = worksheet.Cells[row, 4].Value;
                    var colunaCkArredondado = worksheet.Cells[row, 5].Value;

                    if (colunaMenorPotencia == null) { continue; }

                    var linhaCalcularCk = new CalcularCk();

                    linhaCalcularCk.MenorPotencia = (Convert.ToDouble(colunaMenorPotencia));
                    linhaCalcularCk.TensaoLinha = (Convert.ToDouble(colunaTensaoLinha));
                    linhaCalcularCk.RelacaoTc = (Convert.ToDouble(colunaRelacaoTc));
                    linhaCalcularCk.ValorCk = (Convert.ToDouble(colunaCk));
                    linhaCalcularCk.ValorCkArredondado = (Convert.ToDouble(colunaCkArredondado));

                    listaCalcularFp.Add(linhaCalcularCk);
                }
            }

            return listaCalcularFp;
        }

        public static List<CalcularPotenciaAtiva> LerPlanilhaCalcularPotenciaAtiva(string path)
        {
            var listaCalcularPotenciaAtiva = new List<CalcularPotenciaAtiva>();

            FileInfo existingFile = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["CalcularPotenciaAtiva"];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                var potenciaReativa = worksheet.Cells[1, 1].Value;
                var potenciaAparente = worksheet.Cells[1, 2].Value;
                var potenciaAtiva = worksheet.Cells[1, 3].Value;
                var potenciaAtivaArredondado = worksheet.Cells[1, 4].Value;

                if (!potenciaReativa.Equals("Potência Reativa (kVAr)") ||
                    !potenciaAparente.Equals("Potência Aparente (kVA)") ||
                    !potenciaAtiva.Equals("Potência Ativa (kW)") ||
                    !potenciaAtivaArredondado.Equals("Potência Ativa (kW) (arrendodado 2 casas decimais)")
                    )
                {
                    //Validar
                    return null;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaPotenciaReativa = worksheet.Cells[row, 1].Value;
                    var colunaPotenciaAparente = worksheet.Cells[row, 2].Value;
                    var colunaPotenciaAtiva = worksheet.Cells[row, 3].Value;
                    var colunaPotenciaAtivaArredondado = worksheet.Cells[row, 4].Value;

                    if (colunaPotenciaReativa == null) { continue; }

                    var linhaCalcularPotenciaAtiva = new CalcularPotenciaAtiva();

                    linhaCalcularPotenciaAtiva.PotenciaReativa = (Convert.ToDouble(colunaPotenciaReativa));
                    linhaCalcularPotenciaAtiva.PotenciaAparente = (Convert.ToDouble(colunaPotenciaAparente));
                    linhaCalcularPotenciaAtiva.PotenciaAtiva = (Convert.ToDouble(colunaPotenciaAtiva));
                    linhaCalcularPotenciaAtiva.PotenciaAtivaArredondado = (Convert.ToDouble(colunaPotenciaAtivaArredondado));

                    listaCalcularPotenciaAtiva.Add(linhaCalcularPotenciaAtiva);
                }
            }

            return listaCalcularPotenciaAtiva;
        }

        public static List<VerificarTipoReativoFpCorrigido> LerPlanilhaVerificarTipoReativoFpCorrigido(string path)
        {
            var listaVerificarTipoReativoFpCorrigido = new List<VerificarTipoReativoFpCorrigido>();

            FileInfo existingFile = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["VerificarTipoReativoFpCorrigido"];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                var potenciaReativa = worksheet.Cells[1, 1].Value;
                var tipoFp = worksheet.Cells[1, 2].Value;
                var somaReativoEstagiosUtilizados = worksheet.Cells[1, 3].Value;
                var tipoFpCorrigido = worksheet.Cells[1, 4].Value;

                if (!potenciaReativa.Equals("Potência Reativa (kVAr)") ||
                    !tipoFp.Equals("TipoFp") ||
                    !somaReativoEstagiosUtilizados.Equals("SomaReativoEstagiosUtilizados") ||
                    !tipoFpCorrigido.Equals("TipoFpCorrigido")
                    )
                {
                    //Validar
                    return null;
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaPotenciaReativa = worksheet.Cells[row, 1].Value;
                    var colunaTipoFp = worksheet.Cells[row, 2].Value;
                    var colunaSomaReativoEstagiosUtilizados = worksheet.Cells[row, 3].Value;
                    var colunaTipoFpCorrigido = worksheet.Cells[row, 4].Value;

                    if (colunaPotenciaReativa == null) { continue; }

                    var linhaVerificarTipoReativoFpCorrigido = new VerificarTipoReativoFpCorrigido();

                    linhaVerificarTipoReativoFpCorrigido.PotenciaReativa = (Convert.ToDouble(colunaPotenciaReativa));
                    linhaVerificarTipoReativoFpCorrigido.TipoFp = colunaTipoFp.ToString();
                    linhaVerificarTipoReativoFpCorrigido.SomaReativoEstagiosUtilizados = (Convert.ToDouble(colunaSomaReativoEstagiosUtilizados));
                    linhaVerificarTipoReativoFpCorrigido.TipoFpCorrigido = colunaTipoFpCorrigido.ToString();

                    listaVerificarTipoReativoFpCorrigido.Add(linhaVerificarTipoReativoFpCorrigido);
                }
            }

            return listaVerificarTipoReativoFpCorrigido;
        }
        
    }
}
