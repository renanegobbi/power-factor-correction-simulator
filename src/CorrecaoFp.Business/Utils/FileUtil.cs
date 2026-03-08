using CorrecaoFp.Business.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace CorrecaoFp.Business.Utils
{
    public static class FileUtil
    {
        public static LeituraUpload ReadXls(IFormFile file, string path)
        {
            Console.WriteLine($"ReadXls - valor path: {path}");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

            var leituraUpload = new LeituraUpload();
            var medicoesArquivo = new List<Medicao>();
            var erros = new List<string>();
            var indicesLinhasVazias = new List<int>();
            string formatoDataHora = "dd/MM/yyyy HH:mm";

            FileInfo existingFile = new FileInfo(path);
            Console.WriteLine($"ReadXls - valor existingFile.FullName: {existingFile.FullName}");

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = ContaColunasNaoVazias(worksheet);
                Console.WriteLine($"ReadXls - valor colCount: {colCount}");
                int rowCount = worksheet.Dimension.End.Row;
                Console.WriteLine($"ReadXls - valor worksheet.Dimension.End.Row: {worksheet.Dimension.End.Row}");

                if (colCount != 6)
                {
                    //Validar
                    erros.Add("Número de colunas diferente de 6.");
                }

                var tituloDataInicio = worksheet.Cells[1, 1].Value;
                var tituloDataFim = worksheet.Cells[1, 2].Value;
                var tituloPotenciaReativa = worksheet.Cells[1, 3].Value;
                var tituloPotenciaAparente = worksheet.Cells[1, 4].Value;
                var tituloFP = worksheet.Cells[1, 5].Value;
                var tituloTipoFP = worksheet.Cells[1, 6].Value;

                if (!tituloDataInicio.Equals("Data Início"))
                    erros.Add("Não foi encontrada a coluna \"Data Início\".");

                if (!tituloDataFim.Equals("Data Fim"))
                    erros.Add("Não foi encontrada a coluna \"Data Fim\".");

                if (tituloPotenciaReativa is null || !tituloPotenciaReativa.Equals("Potência Reativa (kVAr)"))
                    erros.Add("Não foi encontrada a coluna \"Potência Reativa (kVAr)\".");

                if (tituloPotenciaAparente is null || !tituloPotenciaAparente.Equals("Potência Aparente (kVA)"))
                    erros.Add("Não foi encontrada a coluna \"Potência Aparente (kVA)\".");

                if (tituloFP is null || !tituloFP.Equals("FP"))
                    erros.Add("Não foi encontrada a coluna \"FP\".");

                if (tituloTipoFP is null || !tituloTipoFP.Equals("Tipo de FP"))
                    erros.Add("Não foi encontrada a coluna \"Tipo de FP\".");

                leituraUpload.Erros.AddRange(erros);
                if (leituraUpload.Erros.Any())
                    return leituraUpload;

                for (int row = 2; row <= rowCount; row++)
                {
                    var colunaDataInicio = worksheet.Cells[row, 1].Value;
                    var colunaDataFim = worksheet.Cells[row, 2].Value;
                    var colunaPotenciaReativa = worksheet.Cells[row, 3].Value;
                    var colunaPotenciaAparente = worksheet.Cells[row, 4].Value;
                    var colunaFP = worksheet.Cells[row, 5].Value;
                    var colunaTipoFP = worksheet.Cells[row, 6].Value;

                    var medicao = new Medicao();

                    if (colunaDataInicio is null ||
                       colunaDataFim is null ||
                       colunaPotenciaReativa is null ||
                       colunaPotenciaAparente is null ||
                       colunaFP is null ||
                       colunaTipoFP is null)
                    {
                        //A linha 2 do excel é o primeiro índice da lista (0)
                        indicesLinhasVazias.Add(row - 2);
                        medicoesArquivo.Add(medicao);
                        continue;
                    }

                    //Valida Data Início
                    string colunaDataInicioString = colunaDataInicio.ToString().Trim();
                    if (colunaDataInicioString.Length >= 16)
                    {
                        colunaDataInicioString = colunaDataInicioString.Substring(0, 16);
                    }

                    if (DateTime.TryParseExact(colunaDataInicioString.ToString().Trim(),
                        formatoDataHora,
                        CultureInfo.GetCultureInfo("pt-BR"),
                        DateTimeStyles.None,
                        out DateTime dataHoraDataInicio))
                    {
                        medicao.DataInicio = Convert.ToDateTime(colunaDataInicio);
                    }
                    else
                    {
                        leituraUpload.Erros.Add($"Erro na linha {row}: não foi possível converter \"Data Início\" ({colunaDataInicio.ToString()}).<br />" +
                        $"O formato esperado é \"dd/MM/yyyy HH:mm:ss\". Exemplo: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                        return leituraUpload;
                    }

                    //Valida Data Fim
                    string colunaDataFimString = colunaDataFim.ToString().Trim();
                    if (colunaDataFimString.Length >= 16)
                    {
                        colunaDataFimString = colunaDataFimString.Substring(0, 16);
                    }

                    if (DateTime.TryParseExact(colunaDataFimString.ToString().Trim(),
                        formatoDataHora,
                        CultureInfo.GetCultureInfo("pt-BR"),
                        DateTimeStyles.None,
                        out DateTime dataHoraDataFim))
                    {
                        medicao.DataFim = Convert.ToDateTime(colunaDataFim);
                    }
                    else
                    {
                        leituraUpload.Erros.Add($"Erro na linha {row}: não foi possível converter \"Data Fim\" ({colunaDataFim.ToString()}).<br />" +
                        $"O formato esperado é \"dd/MM/yyyy HH:mm:ss\". Exemplo: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                        return leituraUpload;
                    }

                    ////Valida Potência Reativa
                    if (double.TryParse(colunaPotenciaReativa.ToString(), NumberStyles.Any | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("pt-BR"), out double outPotenciaReativa))
                    {
                        // Sucesso na conversão
                        medicao.PotenciaReativa = (Convert.ToDouble(colunaPotenciaReativa).ArredondarDuasCasasDecimais());
                    }
                    else
                    {
                        // Falha na conversão
                        leituraUpload.Erros.Add($"Erro na linha {row}: não foi possível converter o valor \"Potência Reativa (kVAr)\" ({colunaPotenciaReativa.ToString()}).<br />" +
                        $"O valor esperado deve estar no formato numérico \"1234,56\".");
                        return leituraUpload;
                    }

                    ////Valida Potência Aparente
                    if (double.TryParse(colunaPotenciaAparente.ToString(), /*NumberStyles.Any*/ NumberStyles.Any | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("pt-BR"), out double potenciaAparente))
                    {
                        // Sucesso na conversão
                        medicao.PotenciaAparente = (Convert.ToDouble(colunaPotenciaAparente).ArredondarDuasCasasDecimais());
                    }
                    else
                    {
                        // Falha na conversão
                        leituraUpload.Erros.Add($"Erro na linha {row}: não foi possível converter o valor \"Potência Aparente (kVA)\" ({colunaPotenciaAparente.ToString()}).<br />" +
                        $"O valor esperado deve estar no formato numérico \"1234,56\".");
                        return leituraUpload;
                    }

                    //Validar Potência Aparente se for negativo
                    if (medicao.PotenciaAparente < 0)
                    {
                        leituraUpload.Erros.Add($"Erro na linha {row}: <br />" +
                            $"A Potência Aparente não pode ser um valor negativo.");
                        return leituraUpload;
                    }

                    //Validar se Potência Reativa é maior que a Potência Aparente:
                    if (medicao.PotenciaReativa.ObterValorAbsoluto() > medicao.PotenciaAparente.ObterValorAbsoluto())
                    {
                        leituraUpload.Erros.Add($"Erro na linha {row}: <br />" +
                            $"Potência Reativa igual a {medicao.PotenciaReativa.ObterValorAbsoluto().ToString()} é maior " +
                            $"que a Potência Aparente igual a {medicao.PotenciaAparente}.");
                        return leituraUpload;
                    }

                    ////Valida FP
                    string colunaFPString = colunaFP.ToString().Trim();

                    if (double.TryParse(colunaFPString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("pt-BR"), out double FP))
                    {
                        if (FP >= 0 && FP <= 1)
                        {
                            // Sucesso na conversão e o valor está entre 0 e 1
                            medicao.FatorPotencia = FP.ArredondarDuasCasasDecimais();
                        }
                        else
                        {
                            // O valor não está entre 0 e 1
                            leituraUpload.Erros.Add($"Erro na linha {row}: O valor do FP igual a {FP} não está entre 0 e 1.");
                            return leituraUpload;
                        }
                    }
                    else
                    {
                        // Falha na conversão
                        leituraUpload.Erros.Add($"Erro na linha {row} ao converter \"FP\" igual a {colunaFP.ToString()}.<br />" +
                            $"O valor do FP não é válido.");
                        return leituraUpload;
                    }

                    ////Valida Tipo FP
                    string colunaTipoPotenciaFPString = colunaTipoFP.ToString().Trim();

                    if (colunaTipoPotenciaFPString.ToUpper() == "Cap".ToUpper() || colunaTipoPotenciaFPString.ToUpper() == "Ind".ToUpper())
                    {
                        medicao.TipoFatorPotencia = colunaTipoPotenciaFPString;
                    }
                    else
                    {
                        // Falha na conversão
                        leituraUpload.Erros.Add($"Erro na linha {row} ao converter \"Tipo FP\" igual a {colunaTipoFP.ToString()}.<br />" +
                            $"O valor do Tipo FP não é válido. Espera-se \"Cap\" ou \"Ind\".");
                        return leituraUpload;
                    }

                    medicao.PotenciaAtiva = BancoCapacitorUtil.CalcularPotenciaAtiva(medicao);

                    medicoesArquivo.Add(medicao);
                }
            }

            // Ordena os índices em ordem decrescente
            indicesLinhasVazias.Sort((a, b) => b.CompareTo(a));

            foreach (int indiceLinhaVazia in indicesLinhasVazias)
            {
                medicoesArquivo.RemoveAt(indiceLinhaVazia);
            }

            leituraUpload.Medicoes.AddRange(medicoesArquivo);

            return leituraUpload;
        }

        public static void DeletarArquivo(string caminhoArquivo)
        {
            FileInfo file = new FileInfo(caminhoArquivo);
            if (file.Exists)
            {
                try
                {
                    file.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao excluir o arquivo: {0}", e.Message);
                }
            }
        }

        static int ContaColunasNaoVazias(ExcelWorksheet worksheet)
        {
            int nonEmptyColumnCount = 0;

            // Itera sobre as colunas da primeira linha
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (worksheet.Cells[1, col].Value != null && !string.IsNullOrEmpty(worksheet.Cells[1, col].Text))
                {
                    nonEmptyColumnCount++;
                }
            }

            return nonEmptyColumnCount;
        }

        static int ContaLinhasNaoVazias(ExcelWorksheet worksheet)
        {
            int nonEmptyLineCount = 0;

            // Itera sobre as linhas 
            for (int lin = 2; lin <= worksheet.Dimension.End.Row; lin++)
            {
                if (worksheet.Cells[lin, 2].Value != null && !string.IsNullOrEmpty(worksheet.Cells[lin, 2].Text))
                {
                    nonEmptyLineCount++;
                }
            }

            return nonEmptyLineCount;
        }

    }
}
