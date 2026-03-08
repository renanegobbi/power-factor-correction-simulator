using CorrecaoFp.Business.Comandos.Entrada;
using CorrecaoFp.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrecaoFp.Business.Interfaces
{
    public interface IMedicaoRepository: IRepository<Medicao>
    {
        Task<double> ObterSomaBancoCapacitor();
        Task<Tuple<Medicao[], double>> ProcurarMedicao(ProcurarMedicaoEntrada entrada);
        Task<Tuple<Medicao[], double>> ProcurarMedicaoReativoExcedente(ProcurarMedicaoEntrada entrada);
        Task Adicionar(List<Medicao> entity);
    }
}
