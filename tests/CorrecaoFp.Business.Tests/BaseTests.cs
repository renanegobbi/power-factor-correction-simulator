using CorrecaoFp.Business.Tests.File;
using CorrecaoFp.Business.Tests.Utils;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CorrecaoFp.Business.Tests
{
    public class BaseTests : IDisposable
    {
        public List<QcNecessario> ListQcNecessario { get; set; }
        public BaseTests()
        {
            ListQcNecessario = FileUtil.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "File", "DadosTestes.xlsx"));
        }

        public IEnumerable<object[]> TesteRetorno()
        {
            List<Object[]> teste = new List<Object[]>();

            foreach (var item in ListQcNecessario)
            {
                teste.Add(new object[] {
                    item.PotenciaAtiva,
                    item.FpInicial,
                    item.FpDesejado,
                    item.PotenciaCapacitivaNecessario,
                    item.PotenciaCapacitivaNecessarioArredondado
                });
            }

            return teste;
        }
            public void Dispose()
        {
        }
    }

}