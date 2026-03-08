using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Models.Validations.Documentos
{
    public class ArquivoValidacao
    {
        public const string ExtensaoXlsx = ".xlsx";
        public const long TamanhoArquivoBytes = 2147483648; // 2 gigabytes = 2147483648 bytes
        public const long TamanhoArquivoGigabytes = 4;

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

        public static async Task<bool> QuantidadeColunasValidas(IFormFile formFile)
        {
            using (var stream = new MemoryStream())
            {
                // Copia o conteúdo do IFormFile para o MemoryStream
                await formFile.CopyToAsync(stream);
                stream.Position = 0; // Reseta a posição do stream para o início

                // Abre o arquivo Excel a partir do MemoryStream
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Obtém a primeira planilha
                    var columnCount = worksheet.Dimension.End.Column; // Conta o número de colunas

                    // Retorna true se houver mais de 6 colunas
                    return columnCount > 6;
                }
            }
        }
    }
}