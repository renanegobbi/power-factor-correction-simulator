using CorrecaoFp.Business.Comandos.Entrada;
using CorrecaoFp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Interfaces.Services
{
    public interface IMedicaoService: IDisposable
    {
        Task<List<Medicao>> ObterTodasMedicoes();
        Task<Tuple<Medicao[], double>> ProcurarMedicaoReativoExcedente(ProcurarMedicaoEntrada entrada);
        Task<Tuple<Medicao[], double>> ProcurarMedicao(ProcurarMedicaoEntrada entrada);
        Task Adicionar(List<Medicao> model);
    }
}